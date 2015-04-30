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
        public Image cardImage { get; set; }

        public PicCard(Card card, Image image)
        {
            this.card = card;
            this.cardImage = image;
        }
    }
}
