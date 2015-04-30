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
using System.Windows.Threading;

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
        private DebugMessageClass Log;
        private volatile bool STOP = false;
        DispatcherTimer inpufocus = new DispatcherTimer();
        GameWindow gameWindow = null;

        public MainWindow()
        {
            InitializeComponent();
            usernameInputTextBox.Focus();
            inpufocus.Interval = new TimeSpan(0, 0, 0, 0, 10);
            inpufocus.Tick += inpufocus_Tick;
            Log = new DebugMessageClass(this);

            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            TabC.ItemContainerStyle = s;

            try
            {
                client.Connect(serverEndPoint);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            this.ChatSzal = new Thread(new ThreadStart(ChatComm));
        }

        void inpufocus_Tick(object sender, EventArgs e)
        {
            Input_field.Focus();
            inpufocus.Stop();
        }

        private void ChatComm()
        {
            byte[] msg = new byte[4096];
            int bytesRead;
            NetworkStream clientStream = client.GetStream();

            while (!STOP)
            {
                bytesRead = clientStream.Read(msg, 0, 4096);
                UTF8Encoding encoder = new UTF8Encoding();
                string json = encoder.GetString(msg, 0, bytesRead);
                Message message = JsonConvert.DeserializeObject<Message>(json);
                if (message.head != null && message.head.STATUS.Equals("MSG"))
                {
                    if (message.head.FROM.Equals("SERVER"))
                    {
                        if (message.head.STATUSCODE != null && message.head.STATUSCODE.Equals("SILENT"))
                        {
                            _Log(System.Environment.NewLine + message.body.MESSAGE);
                        }
                        else
                        {
                            _Log(System.Environment.NewLine + "SERVER MESSAGE: " + message.body.MESSAGE);
                        }
                    }

                    else
                    {
                        _Log(message.body.MESSAGE);
                    }

                }
                else if (message.head != null && message.head.STATUS != null && message.head.STATUS.Equals("CARD"))
                {
                    //if (message.body.MESSAGE == null)
                    //_Log("   " + message.body.CARD.color + ",\t" + message.body.CARD.symbol);

                    gameWindow.CardPreprocessor(message);
                }
                else if (message.head != null && message.head.STATUS.Equals("GAMESTARTED"))
                {
                    _Log(System.Environment.NewLine + message.body.MESSAGE);
                    GameStart();
                }
                else if(message.head != null)
                {
                    _Log(message);
                }
            }


            this.Dispatcher.BeginInvoke((Action)(() =>
                {
                    this.Close();
                }));
        }

        private void SendMessage(string msg, bool login_b = false)
        {
            NetworkStream clientStream = client.GetStream();
            Message message = null;

            try
            {
                message = new Message("MSG", UserName, toWho, msg);
            }
            catch (Exception ex)
            {
                _Log(ex.Message);
            }

            string json = JsonConvert.SerializeObject(message);

            UTF8Encoding encoder = new UTF8Encoding();
            byte[] buffer = encoder.GetBytes(json);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            if (message != null && (message.head.STATUS.Equals("COMMAND") && message.body.MESSAGE.Equals("quit")))
            {
                this.Close();
            }
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








        #region >>> WelcomeTab <<<
        private void ChatB_Click(object sender, RoutedEventArgs e)
        {
            if (usernameInputTextBox.Text.Trim().Length == 0)
            {
                háttérlabel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE8, 0x20, 0x32));
                usernameInputTextBox.Text = "";
            }
            else
            {
                háttérlabel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x7B, 0xB4));
                UserName = usernameInputTextBox.Text.Trim();
                TabC.SelectedItem = ChatTab;
                ChatSzal.Start();
                SendMessage(new Message("LOGIN", UserName, "SERVER", UserName));
                inpufocus.Start();
            }
            //new GameWindow().Show();
        }

        private void usernameInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (usernameInputTextBox.Text.Trim().Length > 0)
                {
                    háttérlabel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x42, 0x7B, 0xB4));
                    UserName = usernameInputTextBox.Text.Trim();
                    TabC.SelectedItem = ChatTab;
                    ChatSzal.Start();
                    SendMessage(new Message("LOGIN", UserName, "SERVER", UserName));
                    inpufocus.Start();
                }
                else
                {
                    háttérlabel.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE8, 0x20, 0x32));
                    usernameInputTextBox.Text = "";
                }
            }
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
                if (Input_field.Text.Length > 0)
                {
                    SendMessage(Input_field.Text);
                    Input_field.Text = "";
                }
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
            SendMessage(new Message("COMMAND", "READY", UserName, "SERVER", ""));
            TabC.SelectedItem = ChatTab;
        }

        private void HelpB_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(new Message("HELP", "UNDEFINED", UserName, toWho, ""));
            TabC.SelectedItem = ChatTab;
        }
        #endregion

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

        private void GameStart()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                gameWindow = new GameWindow(client, UserName);
                gameWindow.Show();
            }));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            STOP = true;
        }


    }
}
