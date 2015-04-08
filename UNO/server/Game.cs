using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server;
using System.Windows;

namespace game
{
    class Game
    {
        private List<Player> players;
        private DeckStorageAncestor dropCards;
        private DeckStorageAncestor pullCards;
        private bool _clockWise;
        private int sameDropCards;
        private int _nextPlayerIndex;
        private RuleChecker ruleChecker;

        public Game(List<Player> players)
        {
            this.players = players;
            this.dropCards = new DeckStorageAncestor();
            this.pullCards = new DeckStorageAncestor();
            loadDeck();
            dropCards.addCard(pullCards.deal());
            this.sameDropCards = 1;
            this.clockWise = true;
            this.ruleChecker = new RuleChecker();
        }

        /// <summary>
        /// Kártyalapok betöltése a húzópakliba
        /// </summary>
        public void loadDeck() {
            pullCards = new DataLinkLayer().loadDeck();
        }


        /// <summary>
        /// Játékos x-edik kártyájának lekérése
        /// </summary>
        /// <param name="player">játékos</param>
        /// <param name="index">lap sorszáma</param>
        /// <returns></returns>
        public Card cardIndex(Player player, int index)
        {
            return player.cardIndex(index);
        }


        /// <summary>
        /// Minden játékos kap 8 véletlen lapot a húzópakliból
        /// </summary>
        public void cardDealing() {
            foreach (Player player in players) {
                for (int i = 0; i < 8; i++) {
                    player.addCard(pullCards.deal());
                }
            }
        }

        /// <summary>
        /// Játékos húz valahány véletlen lapot a húzópakliból
        /// </summary>
        /// <param name="player">húzó játékos</param>
        public void pullCard(Player player, int quantity = 1)
        {
            for (int i = 0; i < quantity; i++) {
                player.addCard(pullCards.deal());
            }
        }

        /// <summary>
        /// Játékos eldob egy lapot, ami a dobópakliba kerül
        /// </summary>
        /// <param name="player">a lap dobója</param>
        /// <param name="card">dobni kívánt lap</param>
        public bool dropCard(Player player, Card card)
        {
            Card topDropCard = topDroppedCard();
            if (card.symEquals(topDropCard))
            {
                sameDropCards++;
            }
            else
            {
                sameDropCards = 1;
            }

            if (player.dropCard(card) != null)
            {
                // van nála, szabályossági vizsgálat:
                //--------> 2. iteráció
                // ha szabályos, szedje ki
                if(ruleChecker.symColCheck(topDropCard,card)){  //szinre szin szamra szam check
                    player.removeCard(true);
                    dropCards.addCard(card);
               
                    return true;
                }
            }
            return false;
        }

        public bool unoState(Player player, Card card)
        {
            if (player.getCardNum() == 2)
            {
                if (dropCard(player, card))
                {
                    player.uno = true;
                    return true;
                }
            }

            return false;

        }

        /// <summary>
        /// Lépés a következő játékosra
        /// </summary>
        public Player nextPlayer()
        {
            if (clockWise == true)
            {
                if (nextPlayerIndex < 3)
                {
                    nextPlayerIndex++;
                }
                else
                {
                    nextPlayerIndex = 0;
                }

            }
            else
            {
                if (nextPlayerIndex > 1)
                {
                    nextPlayerIndex--;
                }
                else
                {
                    nextPlayerIndex = 3;
                }
            }

            return players[nextPlayerIndex];
        }

        public bool clockWise
        {
            set { this._clockWise = value; }
            get { return this._clockWise; }
        }

        public int nextPlayerIndex
        {
            set { this._nextPlayerIndex = value; }
            get { return this._nextPlayerIndex; }
        }

        public void toggleClockWise() {
            if (this.clockWise == true)
            {
                this.clockWise = false;
            }
            else
            {
                this.clockWise = true;
            }
        }


        /// <summary>
        /// dobópakli tetején lévő lapot adja vissza
        /// </summary>
        /// <returns></returns>
        public Card topDroppedCard()
        {
            return dropCards.topCard();
        }

        /*
         * EZ NEM IDE FOG KERÜLNI, DE A JÁTÉKMENET ALAPJA
         * 
        public void Gameplay()
        {
            // pullCards feltöltése
            pullCards = new DataLinkLayer().loadDeck();

            // osztás


            bool kilep = false;
            do
            {

                foreach (Player player in players)
                {
                    // Kiír, hogy ő következik és lapjon lapot

                    // Valamit megad a játékos


                    if (player.getCardNum() == 0)
                    {
                        kilep = true;
                        break;
                    }

                }


            } while (kilep != true);
            
        }*/
    }
}
