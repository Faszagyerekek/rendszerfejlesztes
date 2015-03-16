using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class DeckStorageAncestor
    {
        private static Card droppableCard = null;
        private List<Card> CardList;
        enum plus4 { plusz4, plus4 };
        enum plus2 { plusz2, plus2 };
        enum colorchanger { colorchanger, szinvalto, színváltó, color_changer };
        enum switcher { switcher, fordító, fordito, fordulj };
        enum jump { jump, ugorj, kimaradsz, kihagy, kizáró, kizaro, ugró, ugro };


        public DeckStorageAncestor()
        {
            CardList = new List<Card>();
        }

        public void addCard(Card card)
        {
            CardList.Add(card);
        }

        /*
        public Card dropCard(string color, string symbol)
        {
            string [] p4        = Enum.GetNames(typeof(plus4));
            string [] p2        = Enum.GetNames(typeof(plus2));
            string [] colorc    = Enum.GetNames(typeof(colorchanger));
            string [] sw        = Enum.GetNames(typeof(switcher));
            string [] ju        = Enum.GetNames(typeof(jump));
            Card card;

            switch (color)
            {
                case "black":
                    if (partOfEnum(symbol, p4))
                    {
                        card = drpCard(new Card("SPEC", "BLACK", "p4"));
                        return card;
                    } else if(partOfEnum(symbol, colorc))
                    {
                        card = drpCard(new Card("SPEC", "BLACK", "csw"));
                        return card;
                    }
                    return null;
                case "green":
                    for (int i = 0; i < 10; i++ )
                    {
                        card = drpCard(new Card("COMM", "GREEN", i.ToString()));
                        if (card != null){
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = drpCard(new Card("SPEC", "GREEN", "p2"));
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = drpCard(new Card("SPEC", "GREEN", "sw"));
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = drpCard(new Card("SPEC", "GREEN", "ju"));
                        return card;
                    }
                    return null;
                case "red":
                    for (int i = 0; i < 10; i++)
                    {
                        card = drpCard(new Card("COMM", "RED", i.ToString()));
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = drpCard(new Card("SPEC", "RED", "p2"));
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = drpCard(new Card("SPEC", "RED", "sw"));
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = drpCard(new Card("SPEC", "RED", "ju"));
                        return card;
                    }
                    return null;
                case "blue":
                    for (int i = 0; i < 10; i++)
                    {
                        card = drpCard(new Card("COMM", "BLUE", i.ToString()));
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = drpCard(new Card("SPEC", "BLUE", "p2"));
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = drpCard(new Card("SPEC", "BLUE", "sw"));
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = drpCard(new Card("SPEC", "BLUE", "ju"));
                        return card;
                    }
                    return null;
                case "yellow":
                    for (int i = 0; i < 10; i++)
                    {
                        card = drpCard(new Card("COMM", "YELLOW", i.ToString()));
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = drpCard(new Card("SPEC", "YELLOW", "p2"));
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = drpCard(new Card("SPEC", "YELLOW", "sw"));
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = drpCard(new Card("SPEC", "YELLOW", "ju"));
                        return card;
                    }
                    return null;
                default:
                    return null;
            }
        }
        */
        public Card deal()
        {
            Random x = new Random();
            int i = x.Next(0, CardList.Count);
            Card card = CardList[i];
            droppableCard = card;
            removeCard(true);
            droppableCard = null;
            return card;
        }

        public int getCardNum()
        {
            return CardList.Count;
        }

        public Card topCard (){
            return CardList[-1];
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
        private Card dropCard(Card card)
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
