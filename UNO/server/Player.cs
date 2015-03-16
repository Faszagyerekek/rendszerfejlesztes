using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;

namespace server
{
    class Player
    {
        private bool _ready2play;
        private string _username, _password;
        private int _errPoint;

        private DeckStorageAncestor Hand;

        public Player() { }

        /// <summary>
        /// Felüldefiniált konstruktor (játszani akar-e, felhazsnálónév, jelszó, hibapontszám)
        /// </summary>
        /// <param name="ready2play">A játékos kész-e a játékra</param>
        /// <param name="username">A játékos felhsználó neve</param>
        /// <param name="password">A játékos jelszava</param>
        /// <param name="errPoint">A játszmák során felhalmozódott hibapont</param>
        /// <param name="Hand">A játékos által birtokolt lapok</param>

        public Player(bool ready2play, string username, string password, int errPoint = 0)
        {
            this.ready2play = ready2play;
            this.username = username;
            this.password = password;
            this.errPoint = errPoint;
        }

        public bool ready2play
        {
            set {this._ready2play = value; }
            get {return this._ready2play; }
        }

        public string username
        {
            set { this._username = value; }
            get { return this._username; }
        }

        public string password
        {
            set { this._password = value; }
            get { return this._password; }
        }

        public int errPoint
        {
            set { this._errPoint = value; }
            get { return this._errPoint; }
        }

        public void addCard(Card card)
        {
            Hand.addCard(card);
        }

        public void dropCard(Card card)
        {
            Hand.dropCard(card);
        }

        public int getCardNum()
        {
            return Hand.getCardNum();
        }

        public Card cardIndex(int index)
        {
            return Hand.cardIndex(index);
        }

    }
}
