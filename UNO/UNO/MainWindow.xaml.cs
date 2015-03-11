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


        public MainWindow()
        {
            InitializeComponent();
            Log = new DebugMessageClass(this);
            try
            {
                ///<summary>
                ///Kivételes eset, itt most a kapcsolódást a szerverhez is
                ///ez a debugos osztály végzi
                ///</summary>
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

        private void SendMessage(string msg)
        {
            NetworkStream clientStream = client.GetStream();

            UTF8Encoding encoder = new UTF8Encoding();
            byte[] buffer = encoder.GetBytes(msg);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
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
