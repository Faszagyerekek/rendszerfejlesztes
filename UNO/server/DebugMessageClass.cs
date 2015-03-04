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

        internal void ClientConnected(System.Windows.Controls.TextBox MSGBOX, int c)
        {
            string s = System.Environment.NewLine + ">> Client connected!" + System.Environment.NewLine
                + ">> Amount of connected clients: " + c + System.Environment.NewLine;
            MSGBOX.Text += s;
            MessageBox.Show(s);
        }

        

        internal void ServerStart(System.Windows.Controls.TextBox MSGBOX)
        {
            MSGBOX.Text = "### SERVER ###";
            MSGBOX.Text += System.Environment.NewLine + "### Server started...###" + System.Environment.NewLine
                + "### Waiting for clients ###" + System.Environment.NewLine;
        }
    }
}
