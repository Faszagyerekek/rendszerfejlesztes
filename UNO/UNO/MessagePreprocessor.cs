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
                if (msg.Equals("hand") || msg.Equals("kez") || msg.Equals("kéz"))
                {
                    message = new Message("COMMAND", "HAND" ,username, "SERVER", msg);
                }
                else if (msg.Equals("top") || msg.Equals("utolso") || msg.Equals("utolsó") || msg.Equals("felso") || msg.Equals("felső"))
                {
                    message = new Message("COMMAND", "TOP", username, "SERVER", msg);
                }
                else if (msg.Equals("draw") || msg.Equals("huz") || msg.Equals("húz") || msg.Equals("huzas") || msg.Equals("húzás"))
                {
                    message = new Message("COMMAND", "DRAW", username, "SERVER", msg);
                }
                else if (msg.Equals("ready") || msg.Equals("játék") || msg.Equals("jatek") || msg.Equals("kesz") || msg.Equals("kész"))
                {
                    message = new Message("COMMAND", "READY", username, "SERVER", msg);
                }
                else if (msg.Equals("ok") || msg.Equals("rendben") || msg.Equals("jó") || msg.Equals("jo") || msg.Equals("oke") || msg.Equals("oké"))
                {
                    message = new Message("COMMAND", "OK", username, "SERVER", msg);
                }
                else
                {
                    message = new Message("COMMAND", "UNDEFINED", username, toWho, msg);
                }
            }
            else if (msg.Substring(0, 1) == "?")
            {
                msg = msg.Substring(1, msg.Length - 1);
                if (msg.Equals("commands") || msg.Equals("parancsok"))
                {
                    message = new Message("HELP", "COMMANDS", username, toWho, msg);
                }
                else
                {
                    message = new Message("HELP", "UNDEFINED", username, toWho, msg);
                }
                
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
                    message = new Message("ERROR", username, "SERVER", "Card doesn't exist");
                }
            }
            else if (msg.Equals("LOGIN"))
            {
                message = new Message("LOGIN", username, "SERVER", username);
            }
            else
            {
                MessageBox.Show("Type help, to get ?help... {(;)}");
                message = new Message("UNDEFINED", "UNDEFINED", username, "*", "undefined");
            }
            return message;
        }
    }
}
