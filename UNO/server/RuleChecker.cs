using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;
namespace server
{
    class RuleChecker//nincs is adattagja
    {
       public  RuleChecker()//kell ide egyeltalán valami?
        {

        }

        /// <summary>
        /// két lapot kap: onTop, toPlace; lehet-e a toPlace-t a onTop-ra rakni
        /// </summary>
        /// <param name="onTop">dobópakli felső lapja</param>
        /// <param name="toPlace">eldobni kívánt lap</param>

        public bool symColCheck(Card onTop, Card toPlace)
        {
            if (onTop.color.Equals("BLACK") ||//feketére mindent
                toPlace.color.Equals("BLACK")||//fekete mindenre
                onTop.color.Equals(toPlace.color)||//szin egyezik
                onTop.symbol.Equals(toPlace.symbol)//szimbolum egyezik
                )
            {
                return true;
            }
            return false;
        }

        public bool symPlusCheck(Card onTop, Card toPlace)
        {
            if (onTop.symbol.Equals(toPlace.symbol) || (onTop.symbol.Equals("plus2") && toPlace.symbol.Equals("plus4"))) // ezt már a kontroller lekezeli, de az lenne a szép, ha majd csak itt lenne :)
            {
                return true;
            }
            return false;
        }


    }
}
