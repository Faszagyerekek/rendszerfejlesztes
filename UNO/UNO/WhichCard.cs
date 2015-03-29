using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;
using System.Windows;

namespace UNO
{
    class WhichCard
    {
        enum plus4 { plusz4, plus4 };
        enum plus2 { plusz2, plus2 };
        enum colorchanger { colorchanger, szinvalto, színváltó, color_changer };
        enum switcher { switcher, fordító, fordito, fordulj };
        enum jump { jump, ugorj, kimaradsz, kihagy, kizáró, kizaro, ugró, ugro };

        public WhichCard() { }




        // That's the card you want!
        //Nyilván még be kéne felyezni :D
        public Card thats(string msg)
        {
            string[] p4 = Enum.GetNames(typeof(plus4));
            string[] p2 = Enum.GetNames(typeof(plus2));
            string[] colorc = Enum.GetNames(typeof(colorchanger));
            string[] sw = Enum.GetNames(typeof(switcher));
            string[] ju = Enum.GetNames(typeof(jump));
            string[] asdf = msg.Split(',');
            Card card = null;
            string color  = asdf[0]
                ,  symbol = asdf[1];
            switch (color)
            {
                case "black":
                    if (partOfEnum(symbol, p4))
                    {
                        card = new Card("SPEC", "BLACK", "plus4");
                        return card;
                    }
                    else if (partOfEnum(symbol, colorc))
                    {
                        card = new Card("SPEC", "BLACK", "colorchanger");
                        return card;
                    }
                    return null;
                case "fekete":
                    if (partOfEnum(symbol, p4))
                    {
                        card = new Card("SPEC", "BLACK", "plus4");
                        return card;
                    }
                    else if (partOfEnum(symbol, colorc))
                    {
                        card = new Card("SPEC", "BLACK", "colorchanger");
                        return card;
                    }
                    return null;
                case "green":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                        card = new Card("COMM", "GREEN", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "GREEN", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "GREEN", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "GREEN", "jump");
                        return card;
                    }
                    return null;
                case "zold":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                            card = new Card("COMM", "GREEN", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "GREEN", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "GREEN", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "GREEN", "jump");
                        return card;
                    }
                    return null;
                case "zöld":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                            card = new Card("COMM", "GREEN", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "GREEN", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "GREEN", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "GREEN", "jump");
                        return card;
                    }
                    return null;
                case "red":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                        card = new Card("COMM", "RED", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "RED", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "RED", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "RED", "jump");
                        return card;
                    }
                    return null;
                case "piros":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                            card = new Card("COMM", "RED", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "RED", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "RED", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "RED", "jump");
                        return card;
                    }
                    return null;
                case "blue":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                        card = new Card("COMM", "BLUE", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "BLUE", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "BLUE", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "BLUE", "jump");
                        return card;
                    }
                    return null;
                case "kék":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                            card = new Card("COMM", "BLUE", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "BLUE", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "BLUE", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "BLUE", "jump");
                        return card;
                    }
                    return null;
                case "kek":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                            card = new Card("COMM", "BLUE", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "BLUE", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "BLUE", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "BLUE", "jump");
                        return card;
                    }
                    return null;
                case "yellow":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                        card = new Card("COMM", "YELLOW", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "YELLOW", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "YELLOW", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "YELLOW", "jump");
                        return card;
                    }
                    return null;
                case "sarga":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                            card = new Card("COMM", "YELLOW", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "YELLOW", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "YELLOW", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "YELLOW", "jump");
                        return card;
                    }
                    return null;
                case "sárga":
                    for (int i = 0; i < 10; i++)
                    {
                        if (symbol.Equals(i.ToString()))
                            card = new Card("COMM", "YELLOW", i.ToString());
                        if (card != null)
                        {
                            return card;
                        }
                    }
                    if (partOfEnum(symbol, p2))
                    {
                        card = new Card("SPEC", "YELLOW", "plus2");
                        return card;
                    }
                    else if (partOfEnum(symbol, sw))
                    {
                        card = new Card("SPEC", "YELLOW", "switcher");
                        return card;
                    }
                    else if (partOfEnum(symbol, ju))
                    {
                        card = new Card("SPEC", "YELLOW", "jump");
                        return card;
                    }
                    return null;
                default:
                    return null;
            }
        }
        private bool partOfEnum(string sym, string[] t)
        {
            foreach (string s in t)
            {
                if (s.Equals(sym))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
