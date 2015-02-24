using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class MainController
    {
        /// <summary>
        /// <c>MSGBOX</c> A megjelenített szövegdoboz, melyen a kiíratás megtörténik, mintha csak egy sima console app lenne
        /// </summary>
        private System.Windows.Controls.TextBox MSGBOX;


        public MainController(System.Windows.Controls.TextBox MSGBOX)
        {
            // TODO: Complete member initialization
            this.MSGBOX = MSGBOX;
        }
    }
}
