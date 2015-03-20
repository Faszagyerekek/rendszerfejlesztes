using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;

namespace server
{
    class Message
    {
        Head _head;
        Body _body;
        public Message(string status, string from, string to, Card card)
        {
            _head = new Head(status, from, to);
            _body = new Body(card);
        }

        public Message(string status, string from, string to, string message)
        {
            _head = new Head(status, from, to);
            _body = new Body(message);
        }

        /// <summary>
        /// body - visszaadka a body elemet, de ITT body()-ként kell használni!!!
        /// </summary>
        /// <returns></returns>
        public Body body()
        {
            return this._body;
        }

        /// <summary>
        /// head - visszaadja a head elemet, de ITT head()-ként kell használni!!!
        /// </summary>
        /// <returns>visszaadja a message head elemét</returns>
        public Head head()
        {
            return this._head;
        }

        public class Head
        {
            private string _STATUS, _FROM, _TO;

            public Head(string status, string from, string to)
            {
                this.status = status;
                this.from = from;
                this.to = to;
            }

            public Head() { }

            public string status
            {
                set { this._STATUS = value; }
                get { return this._STATUS; }
            }

            public string from
            {
                set { this._FROM = value; }
                get { return this._FROM; }
            }

            public string to
            {
                set { this._TO = value; }
                get { return this._TO; }
            }
        }

        public class Body
        {
            private Card _card = null;
            private string _message = "";

            public Body(Card card)
            {
                this._card = card;
            }

            public Body(string message)
            {
                this._message = message;
            }

            public Body() { }

            public Card card
            {
                set { this._card = value; }
                get { return this._card; }
            }
            public string message
            {
                set { this._message = value; }
                get { return this._message; }
            }
        }
    }
}
