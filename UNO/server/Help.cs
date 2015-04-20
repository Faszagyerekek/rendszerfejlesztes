using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Help
    {
        public string Generals()
        {
            return System.Environment.NewLine
                + System.Environment.NewLine + "To send a message, type # and the message"
                + System.Environment.NewLine + "   example: #hi guys, what's up?"
                + System.Environment.NewLine + "To send a command, type ! and the command"
                + System.Environment.NewLine + "   example: !hand (it will show you your cards)"
                + System.Environment.NewLine + "To get help, type ? and the help what you need"
                + System.Environment.NewLine + "   example: ?play"
                + System.Environment.NewLine + "To drop a card, type card: and your card with color, and symbol"
                + System.Environment.NewLine + "   example: card:yellow,2"
                + System.Environment.NewLine + "To drop a card, and be in UNO state, type uno: and your card with color, and symbol"
                + System.Environment.NewLine + "   example: uno:blue,jump"
                + System.Environment.NewLine + System.Environment.NewLine +
                "Have fun!";
        }

        public string Commands()
        {
            return System.Environment.NewLine
                + System.Environment.NewLine + "The commands are:"
                + System.Environment.NewLine + "   !hand / !kez / !kéz - with this command, you can get your cards (in your hand)"
                + System.Environment.NewLine + "   !top / !felso / !felső / !utolso / !utolsó - you can get the last dropped card"
                + System.Environment.NewLine + "   !draw / !huz / !húz / !huzas / !húzás - you can draw a card, if you have to"
                + System.Environment.NewLine + "   !ready / !jatek / !játék / !kesz / !kész - you can apply for a game"
                + System.Environment.NewLine + "   !ok - accept penalty"
                + System.Environment.NewLine + "   !color - choose a new color, like this: !color:red"
                + System.Environment.NewLine
            ;
        }

        public string Helps()
        {
            return "";
        }

        public string Cards()
        {
            return "";
        }
    }
}
