using game;
using server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UNO
{
    class MessagePreprocessor
    {

        public Message preprocessing(string username, string toWho, string msg)
        {
            Card card = null;
            Message message = null;
            // Megnézem, hogy a kapott string, az miféle üzenet
            // 0. indexen milyen módosító van
            // 1. indextől következik a szöved, az mi...

            // 0. indexű módosító vizsgálata:

            

            if (msg.Substring(0, 1) == "#")
            {
                msg = msg.Substring(1, msg.Length - 1);
                message = new Message("MSG", username, toWho, msg);
            }
            else if (msg.Substring(0, 1) == "!")
            {
                msg = msg.Substring(1, msg.Length - 1);
                message = new Message("COMMAND", username, toWho, msg);
            }
            else if (msg.Substring(0, 1) == "?")
            {
                msg = msg.Substring(1, msg.Length - 1);
                message = new Message("HELP", username, toWho, msg);
            }
            else if (msg.Length >= 5 && msg.Substring(0, 5) == "card:")
            {
                msg = msg.Substring(5, msg.Length - 5);
                card = new WhichCard().thats(msg);
                if (card != null)
                {
                    message = new Message("CARD", "CARD", username, "SERVER", card);
                }
                else
                {
                    message = new Message("ERROR", username, "SERVER", "Card doesn't exists");
                }
                
            }
            else if (msg.Length >= 4 && msg.Substring(0, 4) == "uno:")
            {
                msg = msg.Substring(4, msg.Length - 4);
                card = new WhichCard().thats(msg);
                if (card != null)
                {
                    message = new Message("CARD", "UNO" , username, "SERVER", card);
                }
                else
                {
                    message = new Message("ERROR", username, "SERVER", "Card doesn't exists");
                }
            }
            else
            {
                MessageBox.Show("Type help, to get ?help... {(;)}");
                message = new Message();
            }
            return message;
        }
    }
}
