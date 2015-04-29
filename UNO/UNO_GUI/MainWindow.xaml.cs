using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Threading;
using System.Net.Sockets;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace UNO_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string UserName = "";
        private Thread ChatSzal;
        private TcpClient client = new TcpClient();
        private IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);
        static private string toWho = "*";

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                client.Connect(serverEndPoint);
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            this.ChatSzal = new Thread(new ThreadStart(ChatComm));
        }

        private void ChatComm()
        {
            byte[] msg = new byte[4096];
            int bytesRead;
            NetworkStream clientStream = client.GetStream();

            MSGBOX.Text += System.Environment.NewLine + "asdfasdfasdf";

            while (true)
            {
                bytesRead = clientStream.Read(msg, 0, 4096);
                UTF8Encoding encoder = new UTF8Encoding();
                string json = encoder.GetString(msg, 0, bytesRead);
                Message message = JsonConvert.DeserializeObject<Message>(json);
                if (message.head.STATUS.Equals("MSG"))
                {
                    if (message.head.FROM.Equals("SERVER"))
                    {
                        if (message.head.STATUSCODE != null && message.head.STATUSCODE.Equals("SILENT"))
                        {
                            MSGBOX.Text += System.Environment.NewLine + message.body.MESSAGE;
                        }
                        else
                        {
                            MSGBOX.Text += System.Environment.NewLine + "SERVER MESSAGE: " + message.body.MESSAGE;
                        }
                    }
                    else
                    {

                    }
                }
                else if (message.head.STATUS.Equals("CARD"))
                {

                }
                else
                {

                }
            }
        }










        #region >>> WelcomeTab <<<
        private void ChatB_Click(object sender, RoutedEventArgs e)
        {
            if (usernameInputTextBox.Text.Length == 0)
            {
                háttérlabel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE8, 0x20, 0x32));
            }
            else
            {
                háttérlabel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x7B, 0xB4));
                UserName = usernameInputTextBox.Text;
                TabC.SelectedItem = ChatTab;
                ChatSzal.Start();
            }
            //new GameWindow().Show();
        }

        #endregion


        #region >>> ChatTab <<<
        private void MSGBOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            MSGBOX.ScrollToEnd();
        }

        private void Input_field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

            }
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabC.SelectedItem = MenuTab;
        }
        #endregion

        #region >>> MenuTab <<<
        private void BackB_Click(object sender, RoutedEventArgs e)
        {
            TabC.SelectedItem = ChatTab;
        }

        private void ExitB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PlayB_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HelpB_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
