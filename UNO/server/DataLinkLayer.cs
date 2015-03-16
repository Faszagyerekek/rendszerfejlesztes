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
               new System.IO.StreamReader("Deck.txt");//hát, öh, nemtom ez így jó-e
            while ((line = file.ReadLine()) != null)
            {
                string [] tmp=line.Split('|');
                ret.addCard(new Card(tmp[0], tmp[1], tmp[2], int.Parse(tmp[3])));

            }

            file.Close();
            //olvasas vege


            return ret;
        }
    }
}
