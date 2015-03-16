using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using game;

namespace server
{
    class DataLinkLayer
    {
        public DataLinkLayer() { }

        //TO DO:  public DSA loadDeck(){}

        //vannak egyeltalán ennek paraméterei?

        public DeckStorageAncestor loadDeck()
        {
            DeckStorageAncestor ret = new DeckStorageAncestor();
            //olvasas
            string line;

            System.IO.StreamReader file =
               new System.IO.StreamReader("deck.txt");
            while ((line = file.ReadLine()) != null)
            {
                string [] tmp=line.Split('|');


            }

            file.Close();
            //olvasas vege


            return ret;
        }
    }
}
