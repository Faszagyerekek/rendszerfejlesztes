using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace server
{
    class DebugMessageClass
    {
        private MainWindow win;

        public DebugMessageClass(MainWindow mainWindow)
        {
            this.win = mainWindow;
        }

        internal void ClientConnected(int c)
        {
            string s = System.Environment.NewLine + ">> Client connected!" + System.Environment.NewLine
                + ">> Amount of connected clients: " + c + System.Environment.NewLine;
            win.MSGBOX.Text += s;
        }

        

        internal void ServerStart()
        {
            win.MSGBOX.Text = "### SERVER ###";
            win.MSGBOX.Text += System.Environment.NewLine + "### Server started...###" + System.Environment.NewLine
                + "### Waiting for clients ###" + System.Environment.NewLine;
        }

        internal void ClientStart()
        {
            win.MSGBOX.Text += System.Environment.NewLine + "### Connecting to the server ###" + System.Environment.NewLine;
            win.MSGBOX.Text += "### Connected to the server ###" + System.Environment.NewLine;
        }

        internal void Popup(string text)
        {
            MessageBox.Show(text);
        }

        internal void Message(string text)
        {
            win.MSGBOX.Text += System.Environment.NewLine + ">>" + text;
        }

        internal void Message(Message message)
        {
            if (message.head != null && message.body != null)
            {
                win.MSGBOX.Text += System.Environment.NewLine + ">>>>";
                win.MSGBOX.Text += System.Environment.NewLine + ">> STATUS:  " + message.head.STATUS;
                win.MSGBOX.Text += System.Environment.NewLine + ">> STATUSC:  " + message.head.STATUSCODE;
                win.MSGBOX.Text += System.Environment.NewLine + ">> FROM:    " + message.head.FROM;
                win.MSGBOX.Text += System.Environment.NewLine + ">> TO:      " + message.head.TO;
                if (message.head.STATUS.Equals("UNO") || message.head.STATUS.Equals("CARD"))
                {
                    win.MSGBOX.Text += System.Environment.NewLine + ">> CARD:    ";
                    win.MSGBOX.Text += System.Environment.NewLine + ">>> Color:  " + message.body.CARD.color;
                    win.MSGBOX.Text += System.Environment.NewLine + ">>> Symbol: " + message.body.CARD.symbol;
                }
                else
                {
                    win.MSGBOX.Text += System.Environment.NewLine + ">> MESSAGE: " + message.body.MESSAGE;
                }
                win.MSGBOX.Text += System.Environment.NewLine + ">>>>";
            }
        }
    }
}
