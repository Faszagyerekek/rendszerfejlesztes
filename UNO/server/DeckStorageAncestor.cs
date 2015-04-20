using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace game
{
    class DeckStorageAncestor
    {
        private static Card droppableCard = null;
        private List<Card> CardList;
        /*
        enum plus4 { plusz4, plus4 };
        enum plus2 { plusz2, plus2 };
        enum colorchanger { colorchanger, szinvalto, színváltó, color_changer };
        enum switcher { switcher, fordító, fordito, fordulj };
        enum jump { jump, ugorj, kimaradsz, kihagy, kizáró, kizaro, ugró, ugro };
        */

        public DeckStorageAncestor()
        {
            CardList = new List<Card>();
        }

        public void addCard(Card card)
        {
            CardList.Add(card);
        }

        public Card deal()
        {
            //Random.Org.Random x = new Random.Org.Random();
            System.Random x = new System.Random(DateTime.Now.Millisecond);
            int i = x.Next(0, CardList.Count);
            Card card = CardList[i];
            droppableCard = card;
            removeCard(true);
            droppableCard = null;
            return card;
        }

        public List<Card> getCardList()
        {
            List<Card> cpy = new List<Card>(CardList);
            return cpy;
        }

        public int getCardNum()
        {
            return CardList.Count;
        }

        public Card topCard (){
            if (CardList.Count > 0)
            {
                return CardList[CardList.Count-1];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// része-e a megadott "név" annak, amiből építkezünk?
        /// </summary>
        /// <param name="sym">szimbólum</param>
        /// <param name="t">az enumhoz tartozó tömb, amiben keresgélek</param>
        /// <returns>igen - létezik ilyen kártya, nem - valamit elgépeltél, vagy nincs</returns>
        private bool partOfEnum(string sym, string[] t)
        {
            foreach(string s in t){
                if (s.Equals(sym)){
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// konkrétan megnézi, hogy a tárolóban az a kártya szerepel-e, ha igen, akkor visszaadja
        /// </summary>
        /// <param name="card">ezt a dropCard-tól kapja, arra létrehozva, amit a tárolóban keresek</param>
        /// <returns>ha kártyát ad vissza, bent a pakliban, ha nem akkor null-t</returns>
        public Card dropCard(Card card)
        {
            foreach (Card c in CardList)
            {
                if (card.Equals(c)){
                    droppableCard = c;
                    return c;
                }
            }
            return null;
        }

        public Card cardIndex( int index )
        {
            return CardList[index];
        }


        /// <summary>
        /// A játéktól megkapja, hogy a letenni kívánt lap szabályos-e, majd ki is szedi a tárolóból a kártyát
        /// </summary>
        /// <param name="regular">A játék osztálytól kapott változó, hogy kiszedheti-e
        ///                         ha false, akkor nem szedi ki a kártyát a pakliból</param>
        public void removeCard(bool regular)
        {
            if (regular){
                CardList.Remove(droppableCard); // CSAK AKKOR, HA SZABÁLYOS!!!!
            }
        }

        

    }
}
