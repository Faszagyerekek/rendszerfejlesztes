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
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Interop;
using System.Drawing;

namespace UNO_GUI
{

    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        TcpClient client = null;
        string username = null;
        private ObservableCollection<PicCard> handCards { get; set; }
        public GameWindow(TcpClient client, string username)
        {
            this.handCards = new ObservableCollection<PicCard>();
            this.DataContext = handCards;
            InitializeComponent();
            player0.ItemsSource = handCards;
            this.username = username;
            this.client   = client;
            SendMessage(new Message("COMMAND", "TOP", username, "SERVER", ""));
            
            SendMessage(new Message("COMMAND", "HAND", username, "SERVER", ""));
        }

        

        public void CardPreprocessor(Message message)
        {
            if (message.head.STATUSCODE == null)
            {
                MessageBox.Show("ERROR - head.statuscode == null");
            }
            else
            {
                if (message.head.STATUSCODE.Equals("HAND"))
                {
                    if (message.body.MESSAGE != null && message.body.MESSAGE.Equals("ß"))
                    {
                        for (int i = 0; i < handCards.Count; i++)
                        {
                            handCards.RemoveAt(i);
                        }
                    }
                    else
                    {
                        try
                        {
                            handCards.Add(new PicCard(message.body.CARD, picSearch(message.body.CARD)));
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                }
                else if (message.head.STATUSCODE.Equals("TOP"))
                {
                    var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                            picSearch(message.body.CARD).GetHbitmap(),
                            IntPtr.Zero,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromEmptyOptions()
                        );
                    topcardPic.Background = new ImageBrush(bitmapSource);
                }
                else if (message.head.STATUSCODE.Equals("REFRESH"))
                {
                    SendMessage(new Message("COMMAND", "TOP", username, "SERVER", ""));


                    SendMessage(new Message("COMMAND", "HAND", username, "SERVER", ""));
                }
            }
            
        }



        private System.Drawing.Bitmap picSearch(Card card)
        {
            switch (card.color)
            {
                case "BLACK":
                    switch (card.symbol)
                    {
                        case "plus4":
                            return UNO_GUI.Properties.Resources.PLUS4;
                        case "colorchanger":
                            return UNO_GUI.Properties.Resources.COLORCHNAGER;
                    }
                    break;
                case "BLUE":
                    switch (card.symbol)
                    {
                        case "0":
                            return UNO_GUI.Properties.Resources.B0;
                        case "1":
                            return UNO_GUI.Properties.Resources.B1;
                        case "2":
                            return UNO_GUI.Properties.Resources.B2;
                        case "3":
                            return UNO_GUI.Properties.Resources.B3;
                        case "4":
                            return UNO_GUI.Properties.Resources.B4;
                        case "5":
                            return UNO_GUI.Properties.Resources.B5;
                        case "6":
                            return UNO_GUI.Properties.Resources.B6;
                        case "7":
                            return UNO_GUI.Properties.Resources.B7;
                        case "8":
                            return UNO_GUI.Properties.Resources.B8;
                        case "9":
                            return UNO_GUI.Properties.Resources.B9;
                        case "plus2":
                            return UNO_GUI.Properties.Resources.BPLUS2;
                        case "jump":
                            return UNO_GUI.Properties.Resources.BJUMP;
                        case "switcher":
                            return UNO_GUI.Properties.Resources.BSWITCHER;
                    }
                    break;
                case "YELLOW":
                    switch (card.symbol)
                    {
                        case "0":
                            return UNO_GUI.Properties.Resources.Y0;
                        case "1":
                            return UNO_GUI.Properties.Resources.Y1;
                        case "2":
                            return UNO_GUI.Properties.Resources.Y2;
                        case "3":
                            return UNO_GUI.Properties.Resources.Y3;
                        case "4":
                            return UNO_GUI.Properties.Resources.Y4;
                        case "5":
                            return UNO_GUI.Properties.Resources.Y5;
                        case "6":
                            return UNO_GUI.Properties.Resources.Y6;
                        case "7":
                            return UNO_GUI.Properties.Resources.Y7;
                        case "8":
                            return UNO_GUI.Properties.Resources.Y8;
                        case "9":
                            return UNO_GUI.Properties.Resources.Y9;
                        case "plus2":
                            return UNO_GUI.Properties.Resources.YPLUS2;
                        case "jump":
                            return UNO_GUI.Properties.Resources.YJUMP;
                        case "switcher":
                            return UNO_GUI.Properties.Resources.YSWITCHER;
                    }
                    break;
                case "RED":
                    switch (card.symbol)
                    {
                        case "0":
                            return UNO_GUI.Properties.Resources.R0;
                        case "1":
                            return UNO_GUI.Properties.Resources.R1;
                        case "2":
                            return UNO_GUI.Properties.Resources.R2;
                        case "3":
                            return UNO_GUI.Properties.Resources.R3;
                        case "4":
                            return UNO_GUI.Properties.Resources.R4;
                        case "5":
                            return UNO_GUI.Properties.Resources.R5;
                        case "6":
                            return UNO_GUI.Properties.Resources.R6;
                        case "7":
                            return UNO_GUI.Properties.Resources.R7;
                        case "8":
                            return UNO_GUI.Properties.Resources.R8;
                        case "9":
                            return UNO_GUI.Properties.Resources.R9;
                        case "plus2":
                            return UNO_GUI.Properties.Resources.RPLUS2;
                        case "jump":
                            return UNO_GUI.Properties.Resources.RJUMP;
                        case "switcher":
                            return UNO_GUI.Properties.Resources.RSWITCHER;
                    }
                    break;
                case "GREEN":
                    switch (card.symbol)
                    {
                        case "0":
                            return UNO_GUI.Properties.Resources.G0;
                        case "1":
                            return UNO_GUI.Properties.Resources.G1;
                        case "2":
                            return UNO_GUI.Properties.Resources.G2;
                        case "3":
                            return UNO_GUI.Properties.Resources.G3;
                        case "4":
                            return UNO_GUI.Properties.Resources.G4;
                        case "5":
                            return UNO_GUI.Properties.Resources.G5;
                        case "6":
                            return UNO_GUI.Properties.Resources.G6;
                        case "7":
                            return UNO_GUI.Properties.Resources.G7;
                        case "8":
                            return UNO_GUI.Properties.Resources.G8;
                        case "9":
                            return UNO_GUI.Properties.Resources.G9;
                        case "plus2":
                            return UNO_GUI.Properties.Resources.GPLUS2;
                        case "jump":
                            return UNO_GUI.Properties.Resources.GJUMP;
                        case "switcher":
                            return UNO_GUI.Properties.Resources.GSWITCHER;
                    }
                    break;
            }
            return null;
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


        // Events ------------------------------------------------------------------

        private void deckPic_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SendMessage(new Message("COMMAND", "DRAW", username, "SERVER", ""));
            SendMessage(new Message("COMMAND", "HAND", username, "SERVER", ""));
        }

        private void player0_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // new Message("CARD", "CARD", username, "SERVER", card)

            PicCard selectedCard = (PicCard)player0.SelectedItem;
            SendMessage(new Message("CARD", "CARD", username, "SERVER", selectedCard.card));


            

            SendMessage(new Message("COMMAND", "HAND", username, "SERVER", ""));
        }
    }
}
