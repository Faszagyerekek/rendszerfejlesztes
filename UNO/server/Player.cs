using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;
using System.Threading;
using System.Net.Sockets;

namespace server
{
    class Player
    {
        private string _username, _password;
        private int _errPoint;
        private bool _uno;
        private bool _inTrouble;
        private TcpClient _socket;
        private Thread _clientThread;
        private DeckStorageAncestor Hand;

        public Player() { }

        /// <summary>
        /// Felüldefiniált konstruktor (játszani akar-e, felhazsnálónév, jelszó, hibapontszám)
        /// </summary>
        /// <param name="username">A játékos felhsználó neve</param>
        /// <param name="password">A játékos jelszava</param>
        /// <param name="errPoint">A játszmák során felhalmozódott hibapont</param>
        /// <param name="Hand">A játékos által birtokolt lapok</param>

        public Player(TcpClient socket, Thread clientThread)
        {
            this.username = "";
            this.password = "";
            this.errPoint = 0;
            this.uno = false;
            this.inTrouble = false;
            this.socket = socket;
            this.clientThread = clientThread;
            this.clientThread.Start(socket);
            Hand = new DeckStorageAncestor();
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

        public bool uno
        {
            set { this._uno = value; }
            get { return this._uno; }
        }

        public bool inTrouble
        {
            set { this._inTrouble = value; }
            get { return this._inTrouble; }
        }

        public TcpClient socket
        {
            set { this._socket = value; }
            get { return this._socket; }
        }

        public Thread clientThread
        {
            set { this._clientThread = value; }
            get { return this._clientThread; }
        }

        public void addCard(Card card)
        {
            Hand.addCard(card);
        }

        public Card dropCard(Card card)
        {
            return Hand.dropCard(card);
        }

        public int getCardNum()
        {
            return Hand.getCardNum();
        }

        public Card cardIndex(int index)
        {
            return Hand.cardIndex(index);
        }

        public List<Card> getCardList(){
            return Hand.getCardList();
        }

        public void removeCard(bool regular){
            Hand.removeCard(regular);
        }
    }
}
