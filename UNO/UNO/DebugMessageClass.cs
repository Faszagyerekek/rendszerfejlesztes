using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace server // EZ A KLIEN OLDAL, CSAK HÜLYE VOLTAM (Krisztián)
{
    class DebugMessageClass
    {
        private UNO.MainWindow win;

        public DebugMessageClass(UNO.MainWindow mainWindow)
        {
            // TODO: Complete member initialization
            this.win = mainWindow;
        }

        internal void ClientConnected(int c)
        {
            string s = System.Environment.NewLine + ">> Client connected!" + System.Environment.NewLine
                + ">> Amount of connected clients: " + c + System.Environment.NewLine;
            win.MSGBOX.Text += s;
            MessageBox.Show(s);
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
        }

        internal void Client_ClientConnected()
        {
            win.MSGBOX.Text += "### Connected to the server ###" + System.Environment.NewLine;
        }

        internal void Popup(string text)
        {
            MessageBox.Show(text);
        }

        internal void Message(string text)
        {
            win.MSGBOX.Text += text;
        }
    }
}
