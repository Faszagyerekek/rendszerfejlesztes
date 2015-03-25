﻿using System;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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

            Log.Message("Enter a username!" + System.Environment.NewLine);

            this.ChatSzal = new Thread(new ThreadStart(ChatComm));
            ChatSzal.Start();
        }

        private void ChatComm()
        {
            byte[] msg = new byte[4096];
            int bytesRead;
            NetworkStream clientStream = client.GetStream();

            while (true)
            {
                bytesRead = clientStream.Read(msg, 0, 4096);
                UTF8Encoding encoder = new UTF8Encoding();
                string json = encoder.GetString(msg, 0, bytesRead);
                Message message = JsonConvert.DeserializeObject<Message>(json);
                _Log(message);
            }
        }

        /// <summary>
        /// A felhasználótól érkező stringet előfeldolhozza, majd meghívja rá a küldést
        /// </summary>
        /// <param name="msg"></param>
        private void SendMessage(string msg)
        {
            NetworkStream clientStream = client.GetStream();
            Message message = new Message();
            
            try
            {
                // összeállítja az üzenetet
                message = new MessagePreprocessor().preprocessing(username, toWho, msg);
            }
            catch (Exception ex)
            {
                _Log(ex.Message);
            }
            
            
            string json = JsonConvert.SerializeObject(message);

            _Log(json);

            UTF8Encoding encoder = new UTF8Encoding();
            byte[] buffer = encoder.GetBytes(json);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            if (message.head != null && message.head.STATUS.Equals("COMMAND") && message.body.MESSAGE.Equals("quit")){
                this.Close();
            }
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
                if (Input_field.Text.Length > 0)
                {
                    if (login)
                    {
                        SendMessage(Input_field.Text);
                        Input_field.Text = "";
                    }
                    if (!login)
                    {
                        username = Input_field.Text;
                        login = true;
                        Input_field.Text = "";
                        _Log("--->   " + username + " logged in");
                    }
                }
            }
        }

        private void CLIENT_Closed(object sender, EventArgs e)
        {
            if (ChatSzal.IsAlive)
            {
                ChatSzal.Abort();
            }
            SendMessage("!quit");
        }


//Misc -----------------------------------------------------------------------------------------
        private void _Log(string s)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Log.Message(s + System.Environment.NewLine);
            }));
        }

        private void _Log(Message s)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                Log.Message(s);
            }));
        }

        private void MSGBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            MSGBOX.ScrollToEnd();
        }

    }
}
