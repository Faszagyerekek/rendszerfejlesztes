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


    }
}
