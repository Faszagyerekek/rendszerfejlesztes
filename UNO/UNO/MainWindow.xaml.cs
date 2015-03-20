using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using server;


namespace UNO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Thread ChatSzal;
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
        private DebugMessageClass Log;
        static private string username;
        static private bool login = false;
        static private string toWho = "*";


        public MainWindow()
        {
            InitializeComponent();
            Log = new DebugMessageClass(this);

            Log.Message("Enter a username!" + System.Environment.NewLine);

            try
            {
                Log.ClientStart();
                client.Connect(serverEndPoint);
                Log.Client_ClientConnected();
            }
            catch (Exception)
            {
                throw;
            }
            this.ChatSzal = new Thread(new ThreadStart(ChatComm));
            ChatSzal.Start();
        }

        private void ChatComm()
        {
            byte[] message = new byte[4096];
            int bytesRead;
            NetworkStream clientStream = client.GetStream();

            while (true)
            {
                bytesRead = clientStream.Read(message, 0, 4096);
                UTF8Encoding encoder = new UTF8Encoding();
                string msg = encoder.GetString(message, 0, bytesRead);
                _Log(msg + System.Environment.NewLine);
            }
        }

        /// <summary>
        /// A felhasználótól érkező stringet előfeldolhozza, majd meghívja rá a küldést
        /// </summary>
        /// <param name="msg"></param>
        private void SendMessage(string msg)
        {
            NetworkStream clientStream = client.GetStream();

            Message message = MessagePreprocessor(msg);

            UTF8Encoding encoder = new UTF8Encoding();
            byte[] buffer = encoder.GetBytes(msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }


        /// <summary>
        /// A felhasználótól érkező karaktersorozatot
        /// előfeldolgozza, majd üzenet típusú objektummá alakítja
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Message MessagePreprocessor(string msg)
        {
            Message message;
            // Megnézem, hogy a kapott string, az miféle üzenet
            // 0. indexen milyen módosító van
            // 1. indextől következik a szöved, az mi...

            // 0. indexű módosító vizsgálata:

            if (msg.Substring(0, 1) == "#")
            {
                msg = msg.Substring(1, msg.Length-2);
                message = new Message("MSG", username, toWho, msg);
            }
            else if (msg.Substring(0, 1) == "!")
            {
                msg = msg.Substring(1, msg.Length - 2);
                message = new Message("COMMAND", username, toWho, msg);
            }
            else if (msg.Substring(0, 1) == "?")
            {
                msg = msg.Substring(1, msg.Length - 2);
                message = new Message("HELP", username, toWho, msg);
            }
            else
            {

            }
            return null;
        }









// Events -------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Itt kellene majd megoldani, hogy ne kiírja az MSGBOX-ba, hanem egy szálon felírja a szervernek. Ez a szál lesz majd a Játék szál!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Milyen billentyűt ütött le a kliens a Commandhoz</param>
        private void Input_field_KeyDown(object sender, KeyEventArgs e)
        {
            // e.KeyData != Keys.Enter || e.KeyData != Keys.Return
            if (e.Key == Key.Enter)
            {
                if (!login){
                    username = Input_field.Text;
                }
                SendMessage(Input_field.Text);
                Input_field.Text = "";
            }
        }

        private void CLIENT_Closed(object sender, EventArgs e)
        {
            if (ChatSzal.IsAlive)
            {
                ChatSzal.Abort();
            }
            SendMessage("##<quit>##");
        }


//Misc -----------------------------------------------------------------------------------------
        private void _Log(string s)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Log.Message(s);
            }));
        }

    }
}
