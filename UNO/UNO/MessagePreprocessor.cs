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
            else
            {
                MessageBox.Show("Kártya letétele még nem definiált");
                message = new Message();
            }
            return message;
        }
    }
}
