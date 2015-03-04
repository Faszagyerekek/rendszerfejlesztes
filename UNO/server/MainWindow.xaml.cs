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

namespace server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TcpListener tcpListener;
        private Thread JatekSzal;
        private Thread BroadcastSzal;
        private int connectedClients = 0;


        public MainWindow()
        {
            InitializeComponent();
            new DebugMessageClass().ServerStart(MSGBOX);
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

            while (true) // Never ends until the Server is closed.
            {
                //blocks until a client has connected to the server
                TcpClient client = this.tcpListener.AcceptTcpClient();

                //create a thread to handle communication 
                //with connected client
                connectedClients++; // Increment the number of clients that have communicated with us.
                new DebugMessageClass().ClientConnected(MSGBOX, connectedClients);

                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        }

        private void HandleClientComm(object obj)
        {
            throw new NotImplementedException();
        }




        /// <summary>
        /// Itt fog megtörténni (billentyűzetről) a broadcastolás, ergo, amit a szerver ide beír, az megy mindenkinek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Milyen billentyűt ütött le a szerver a Commandhoz</param>
        private void Input_field_KeyDown(object sender, KeyEventArgs e)
        {
            // e.KeyData != Keys.Enter || e.KeyData != Keys.Return
            if (e.Key == Key.Enter)
            {
                MSGBOX.Text += Input_field.Text;
                MSGBOX.Text += System.Environment.NewLine;
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
        private void ClientReading()
        {

        }
    }
}
