using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class Card
    {
        private string _type, _color, _symbol;
        private int _penaltyPoint;

        public Card() { }

        /// <summary>
        /// Felüldefiniált konstruktor (típus, szín, szimbólum, hibapont)
        /// </summary>
        /// <param name="type">A kártya típusa (általános, vagy speciális)</param>
        /// <param name="color">A kártya színe</param>
        /// <param name="symbol">A kártya száma, vagy szimbóluma</param>
        /// <param name="penaltyPont">A kártyához tartozó hibapont</param>
        public Card(string type, string color, string symbol, int penaltyPont)
        {
            this.type = type;
            this.color = color;
            this.symbol = symbol;
            this.penaltyPoint = penaltyPoint;
        }

        public string type
        {
            set { this._type = value; }
            get { return this._type; }
        }

        public string color
        {
            set { this._color = value; }
            get { return this._color; }
        }

        public string symbol
        {
            set { this._symbol = value; }
            get { return this._symbol; }
        }

        public int penaltyPoint
        {
            set { this._penaltyPoint = value; }
            get { return this._penaltyPoint; }
        }
    }
}
