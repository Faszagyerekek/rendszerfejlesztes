using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using game;

namespace server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpListener tcpListener;
        private Thread JatekSzal;
        private int connectedClients = 0;
        private DebugMessageClass Log;
        private delegate void WriteMessageDelegate(string msg);
        private List<TcpClient> clients;
        private Thread clientThread;
        private Game game;
        private List<Player> playerList;
        private List<Player> readyPlayers;


        public MainWindow()
        {
            InitializeComponent();
            clients = new List<TcpClient>();
            Log = new DebugMessageClass(this);
            playerList = new List<Player>();
            readyPlayers = new List<Player>();
            Server();
        }

        
        private void Server()
        {
            this.tcpListener = new TcpListener(IPAddress.Loopback, 3000); // Change to IPAddress.Any for internet wide Communication
            this.JatekSzal = new Thread(new ThreadStart(ListenForClients));
            this.JatekSzal.Start();
        }

        private void ListenForClients()
        {
            this.tcpListener.Start();
            //new DebugMessageClass().ServerStart(MSGBOX);
            this.Dispatcher.Invoke((Action)(() =>
            {
                Log.ServerStart();
            }));

            while (true) // Never ends until the Server is closed.
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();
                clients.Add(client);

                //create a thread to handle communication 
                //with connected client
                connectedClients++; // Increment the number of clients that have communicated with us.

                this.Dispatcher.Invoke((Action)(() =>
                {
                    Log.ClientConnected(connectedClients);
                }));

                clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object client) // ez lesz maga a játékos
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();

            byte[] fromClient = new byte[4096];
            int bytesRead;
            

            while (true)
            {
// olvas a klienstől ----- 
                bytesRead = 0;
                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(fromClient, 0, 4096);
                }
                catch (Exception ex)
                {
                    //a socket error has occured
                    MessageBox.Show(ex.Message);
                    break;
                }

                if (bytesRead == 0)
                {
                    connectedClients--;
                    _Log(">> Client disconnected");
                    break;
                }

                //message has successfully been received
                //ASCIIEncoding encoder = new ASCIIEncoding();
                UTF8Encoding encoder = new UTF8Encoding();

                // Convert the Bytes received to a string and display it on the Server Screen
                string json = encoder.GetString(fromClient, 0, bytesRead);
                Message message = JsonConvert.DeserializeObject<Message>(json);

                if (message != null && message.head.STATUS.Equals("COMMAND") && message.body.MESSAGE.Equals("quit"))
                {
                    connectedClients--;
                    _Log(">> Client disconnected");
                    break;
                }

// megnézi mit olvasott
                Player player = Identify((TcpClient)client);
                #region >>> Bejött üzenet feldolgozása <<<
                #region ### Kártya ###
                if (message != null)
                {
                    if (message.head.STATUS.Equals("CARD"))
                    {
                        if (message.head.STATUSCODE.Equals("CARD"))
                        {
                            if (player.ID == game.currentPlayer().ID && player.inTrouble == false)
                            {
                                if (game.dropCard(player, message.body.CARD))
                                {
                                    sendMessage(new Message("MSG", "SERVER", player.username, "Card droped"), player);

                                    if (player.getCardNum() == 0)
                                    {
                                        Broadcast(JsonConvert.SerializeObject(new Message("MSG", message.head.FROM, "*", "Game ended. The winner is " + player.username)));
                                        // JÁTSZMALEZÁRÁS, PONTOZÁS
                                    }
                                    else if (player.getCardNum() == 1)
                                    {
                                        game.pullCard(player, 2);
                                        Broadcast(JsonConvert.SerializeObject(new Message("MSG", message.head.FROM, "*", player.username + " has only one card left and did not say uno, so he had to pull 2 cards")));
                                    }

                                    if (message.body.CARD.symbol == "plus2" || message.body.CARD.symbol == "plus4" || message.body.CARD.symbol == "jump")
                                    {
                                        game.nextPlayer().inTrouble = true;

                                        if (message.body.CARD.symbol == "jump")
                                        {
                                            sendMessage(new Message("ERROR", "SERVER", game.currentPlayer().username, "You should place a jump card or you will stay out of the turn"), game.currentPlayer());
                                        }
                                        else
                                        {
                                            sendMessage(new Message("ERROR", "SERVER", game.currentPlayer().username, "You should place a plus card or you have to pull some cards"), game.currentPlayer());
                                        }
                                    }
                                    else if (message.body.CARD.symbol == "colorchanger")
                                    {
                                        sendMessage(new Message("ERROR", "SERVER", player.username, "Please choose a color (red/blue/green/yellow)"), player);
                                        player.inTrouble = true;
                                    }
                                    else if (message.body.CARD.symbol == "switcher")
                                    {
                                        game.toggleClockWise();
                                        game.nextPlayer();
                                    }
                                    else
                                    {
                                        game.nextPlayer();
                                    }
                                }
                                else
                                {
                                    sendMessage(new Message("ERROR", "SERVER", player.username, "You can not place that card"), player);
                                }
                            }
                            else if (player.ID == game.currentPlayer().ID && player.inTrouble == true)
                            {
                                if (message.body.CARD.symbol.Equals(game.topDroppedCard().symbol) || (message.body.CARD.symbol.Equals("plus4") && game.topDroppedCard().symbol.Equals("plus2")))
                                {
                                    if (game.dropCard(player, message.body.CARD))
                                    {
                                        sendMessage(new Message("MSG", "SERVER", player.username, "Card droped"), player);
                                        game.currentPlayer().inTrouble = false;
                                        game.nextPlayer().inTrouble = true;
                                    }
                                }
                                else
                                {
                                    sendMessage(new Message("ERROR", "SERVER", player.username, "You can not place that card"), player);
                                }
                            }
                            else
                            {
                                sendMessage(new Message("ERROR", "SERVER", player.username, "It is not your turn"), player);
                            }
                        }
                        else if (message.head.STATUSCODE.Equals("UNO"))
                        {
                            if (player.ID == game.currentPlayer().ID && player.inTrouble == false)
                            {
                                if (game.unoState(player, message.body.CARD))
                                {
                                    sendMessage(new Message("MSG", "SERVER", player.username, "Card droped, in UNO state"), player);

                                    if (message.body.CARD.symbol == "plus2" || message.body.CARD.symbol == "plus4" || message.body.CARD.symbol == "jump")
                                    {
                                        game.nextPlayer().inTrouble = true;

                                        if (message.body.CARD.symbol == "jump")
                                        {
                                            sendMessage(new Message("ERROR", "SERVER", player.username, "You should place a jump card or you will stay out of the turn"), player);
                                        }
                                        else
                                        {
                                            sendMessage(new Message("ERROR", "SERVER", player.username, "You should place a plus card or you have to pull some cards"), player);
                                        }
                                    }
                                    else if (message.body.CARD.symbol == "switcher")
                                    {
                                        game.toggleClockWise();
                                        game.nextPlayer();
                                    }
                                    else
                                    {
                                        game.nextPlayer();
                                    }
                                }
                                else
                                {
                                    sendMessage(new Message("ERROR", "SERVER", player.username, "You can not place that card and be in uno state"), player);
                                }
                            }
                            else
                            {
                                sendMessage(new Message("ERROR", "SERVER", player.username, "It is not your turn"), player);
                            }
                        }
                    }
                #endregion
                    #region ### Üzenet ###
                    else if (message.head.STATUS.Equals("MSG"))
                    {
                        Broadcast(JsonConvert.SerializeObject(new Message("MSG", message.head.FROM, "*", message.head.FROM + ": " + message.body.MESSAGE)));
                    }
                    #endregion
                    #region ### Kliens oldali hiba ###
                    else if (message.head.STATUS.Equals("ERROR"))
                    {
                        sendMessage(message, player);
                    }
                    #endregion
                    #region ### Parancs ###
                    else if (message.head.STATUS.Equals("COMMAND") && !message.head.STATUSCODE.Equals("UNDEFINED"))
                    {
                        if (message.head.STATUSCODE.Equals("HAND"))
                        {
                            foreach (Card card in player.getCardList())
                            {
                                sendMessage(new Message("CARD", "SERVER", player.username, card), player);
                                Thread.Sleep(50);
                            }
                        }
                        else if (message.head.STATUSCODE.Equals("TOP"))
                        {
                            sendMessage(new Message("CARD", "SERVER", player.username, game.topDroppedCard()), player);
                        }
                        else if (message.head.STATUSCODE.Equals("DRAW"))
                        {
                            if (player.ID == game.currentPlayer().ID && player.inTrouble == false)
                            {
                                game.pullCard(player);
                                sendMessage(new Message("MSG", "SERVER", player.username, "Card added"), player);
                                game.nextPlayer();
                            }
                            else
                            {
                                sendMessage(new Message("ERROR", "SERVER", player.username, "It is not your turn"), player);
                            }
                        }
                        else if (message.head.STATUSCODE.Equals("OK"))
                        {
                            if (player.ID == game.currentPlayer().ID && player.inTrouble == true && game.topDroppedCard().symbol != "colorchanger")
                            {
                                sendMessage(new Message("MSG", "SERVER", player.username, "Penalty accepted"), player);
                                if (game.topDroppedCard().symbol == "plus2" || game.topDroppedCard().symbol == "plus4")
                                {
                                    game.pullCard(player, 2 * game.sameDropCards);
                                }
                                else if (game.topDroppedCard().symbol == "jump")
                                {
                                    game.nextPlayer();
                                }

                                player.inTrouble = false;
                            }
                            else
                            {
                                sendMessage(new Message("ERROR", "SERVER", player.username, "You can not use this command now"), player);
                            }
                        }
                        else if (message.head.STATUSCODE.Equals("COLOR"))
                        {
                            if (player.ID == game.currentPlayer().ID && player.inTrouble == true && game.topDroppedCard().symbol == "colorchanger")
                            {
                                game.setNewColor(message.body.MESSAGE);
                                player.inTrouble = false;

                            }
                            else
                            {
                                sendMessage(new Message("ERROR", "SERVER", player.username, "You can not use this command now"), player);
                            }
                        }
                        else if (message.head.STATUSCODE.Equals("READY"))
                        {
                            readyPlayers.Add(player);
                            sendMessage(new Message("MSG", "SERVER", player.username, "Waiting for other players..."), player);
                            if (readyPlayers.Count == 2)
                            {
                                gamePlay(readyPlayers);
                                Broadcast(JsonConvert.SerializeObject(new Message("MSG", message.head.FROM, "*", "New game started")));
                            }
                        }
                    }
                    # endregion
                    #region ### Segítség ###
                    else if (message.head.STATUS.Equals("HELP"))
                    {
                        if (message.head.STATUSCODE.Equals("COMMAND"))
                        {
                            sendMessage(new Message("MSG", "SERVER", player.username, new Help().Commands()), player);
                        }
                    }
                    #endregion
                    #region ### Bejelentkezés ###
                    else if (message.head.STATUS.Equals("LOGIN"))
                    {
                        playerList.Add(new Player(true, message.head.FROM, "password", clients[clients.Count - 1].Client.Handle.ToInt32()));

                        _Log(System.Environment.NewLine + ">>" + message.head.FROM + " connected" + System.Environment.NewLine);
                        try
                        {
                            sendMessage(new Message(
                                "MSG",
                                "SERVER",
                                playerList[playerList.Count - 1].username,
                                "If you want to play:" + System.Environment.NewLine + new Help().Generals()
                            ), playerList[playerList.Count - 1]);
                        }
                        catch (Exception exc) { }

                    }
                }
                #endregion

                #endregion


            }

            clients.Remove(tcpClient);
            tcpClient.Close();
        }

        private void gamePlay(List<Player> playerList)
        {
            game = new Game(playerList);
            game.cardDealing();
        }

        private Player Identify(TcpClient client)
        {
            foreach (Player player in playerList)
            {
                if (client.Client.Handle.ToInt32() == player.ID)
                {
                    return player;
                }
            }
            return null;
        }

        
        /// <summary>
        /// Itt fog megtörténni (billentyűzetről) a broadcastolás, ergo, amit a szerver ide beír, az megy mindenkinek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Milyen billentyűt ütött le a szerver a Commandhoz</param>
        private void Input_field_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                _Log(Input_field.Text + System.Environment.NewLine);
                Broadcast(JsonConvert.SerializeObject(new Message("MSG", "SERVER", "*", Input_field.Text)));
                Input_field.Text = "";
            }
            else
            {

            }
        }

        /// <summary>
        /// Ez lesz az a függvény, ami majd az egyes kliensekre hallgatózik, vagyis a játszó szál
        /// lényeges megemlíteni, hogy kell-e és milyen argumentumok (valószínűleg szál, vagy kliens azonosító) ebbe a függvénybe
        /// </summary>
        private void Broadcast(string msg)
        {
            foreach (TcpClient client in clients)
            {
                NetworkStream clientStream = client.GetStream();

                UTF8Encoding encoder = new UTF8Encoding();
                byte[] buffer = encoder.GetBytes(msg);

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
            }
        }

        private void sendMessage(Message message , Player player)
        {
            foreach (TcpClient client in clients)
            {
                if (client.Client.Handle.ToInt32() == player.ID)
                {
                    NetworkStream clientStream = client.GetStream();

                    string json = JsonConvert.SerializeObject(message);

                    UTF8Encoding encoder = new UTF8Encoding();
                    byte[] buffer = encoder.GetBytes(json);

                    clientStream.Write(buffer, 0, buffer.Length);
                    clientStream.Flush();
                }
            }
        }

//MISC ------------------------------------------------------------------------------------------------------

        private void SERVER_Closed(object sender, EventArgs e)
        {
            if (JatekSzal.IsAlive){
                JatekSzal.Abort();
            }
            
        }

        private void MSGBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            MSGBOX.ScrollToEnd();
        }

        private void _Log(string s)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Log.Message(s);
            }));
        }

        private void _Log(Message s)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Log.Message(s);
            }));
        }
    }
}
