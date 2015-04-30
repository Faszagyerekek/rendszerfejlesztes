using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Serialization;
using game;

namespace UNO_GUI
{

    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        TcpClient client = null;
        string username = null;
        public List<Card> handCards { get; set; }
        public GameWindow(TcpClient client, string username)
        {
            InitializeComponent();
            handCards = new List<Card>();
            this.username = username;
            this.client   = client;
            SendMessage(new Message("COMMAND", "HAND", username, "SERVER", ""));
            
        }




        private void SendMessage(Message message)
        {
            NetworkStream clientStream = client.GetStream();
            string json = JsonConvert.SerializeObject(message);

            //_Log(json);

            UTF8Encoding encoder = new UTF8Encoding();
            byte[] buffer = encoder.GetBytes(json);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }
    }
}
