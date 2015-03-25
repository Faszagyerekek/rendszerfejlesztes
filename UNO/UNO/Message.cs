using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;

namespace server
{
    public class Head
    {
        public string STATUS { get; set; }
        public string STATUSCODE { get; set; }
        public string FROM { get; set; }
        public string TO { get; set; }
    }

    public class Body
    {
        public game.Card CARD { get; set; }
        public string MESSAGE { get; set; }
    }

    public class Message
    {

        public Message(string p, string username, string toWho, string msg)
        {
            this.head = new Head();
            this.body = new Body();
            // TODO: Complete member initialization
            this.head.STATUS = p;
            this.head.FROM = username;
            this.head.TO = toWho;
            this.body.MESSAGE = msg;
        }

        public Message(string p, string username, string toWho, Card card)
        {
            this.head = new Head();
            this.body = new Body();
            // TODO: Complete member initialization
            this.head.STATUS = p;
            this.head.FROM = username;
            this.head.TO = toWho;
            this.body.CARD = card;
        }

        public Message(string p, string statuscode, string username, string toWho, Card card)
        {
            this.head = new Head();
            this.body = new Body();
            // TODO: Complete member initialization
            this.head.STATUS = p;
            this.head.STATUSCODE = statuscode;
            this.head.FROM = username;
            this.head.TO = toWho;
            this.body.CARD = card;
        }

        public Message(string p, string statuscode, string username, string toWho, string msg)
        {
            this.head = new Head();
            this.body = new Body();
            // TODO: Complete member initialization
            this.head.STATUS = p;
            this.head.STATUSCODE = statuscode;
            this.head.FROM = username;
            this.head.TO = toWho;
            this.body.MESSAGE = msg;
        }


        public Message()
        {
            this.head = null;
            this.body = null;
        }
        public Head head { get; set; }
        public Body body { get; set; }
    }
}
