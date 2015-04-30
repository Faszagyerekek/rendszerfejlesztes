using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;
using System.Drawing;

namespace UNO_GUI
{
    class PicCard : Card
    {
        public Card card { get; set; }
        public Bitmap cardImage { get; set; }

        public PicCard(Card card, Bitmap image = null)
        {
            this.cardImage = UNO_GUI.Properties.Resources.COVER;
            this.card = card;

            if (image != null)
            this.cardImage = image;
        }
    }
}
