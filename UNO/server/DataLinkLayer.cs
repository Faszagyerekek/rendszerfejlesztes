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
            var text =server.Properties.Resources.Deck;// a valami egy egesz string
           


            string[] sorok = text.Split('#');

            foreach (string sor in sorok)
            {
                string[] tmp = sor.Split('|');
                ret.addCard(new Card(tmp[0], tmp[1], tmp[2], int.Parse(tmp[3])));
            }
                
               
            
            System.Windows.MessageBox.Show("lel");
           
            //olvasas vege


            return ret;
        }
        /*
        public List<Record> loadHOF()
        {
            List<Record> ret = new List<Record>();
            //olvasas
            string line;

            System.IO.StreamReader file =
               new System.IO.StreamReader("HOF");//hát, öh, nemtom ez így jó-e
            //ilyen állományunk még nincs is te balfasz
            //aztán ezt nehogy megint benhagyd, hallod
            ///
            //
            //
            //
            //vedd ki te fos!
            //
            //
            //
            //

            while ((line = file.ReadLine()) != null)
            {
                string[] tmp = line.Split('|');
                ret.Add(new Record(tmp[0],int.Parse(tmp[1])));
                Ite

            }

            file.Close();
            //olvasas vege


            return ret;
        }
        */
    }
}
