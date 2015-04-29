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
        private int _cardToPull;
        private int _currentPlayerIndex;
        private RuleChecker ruleChecker;

        public Game(List<Player> players)
        {
            this.players = new List<Player>();
            for (int i = 0; i < players.Count(); i++)
            {
                this.players.Add(players[i]);
            }

            this.dropCards = new DeckStorageAncestor();
            this.pullCards = new DeckStorageAncestor();
            loadDeck();
            do
            {
                dropCards.addCard(pullCards.deal());
            } while (topDroppedCard().type != "COMM");
            this.cardToPull = 0;
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
                for (int i = 0; i < 7; i++) {
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

            if (player.dropCard(card) != null)
            {
                // van nála, szabályossági vizsgálat:
                //--------> 2. iteráció
                // ha szabályos, szedje ki
                if(!player.inTrouble && ruleChecker.symColCheck(topDropCard,card)){  //szinre szin szamra szam check

                    if (card.symbol == "plus4")
                    {
                        cardToPull += 4;
                    }
                    else if (card.symbol == "plus2")
                    {
                        cardToPull += 2;
                    }


                    player.removeCard(true);
                    dropCards.addCard(card);
               
                    return true;
                }
                else if (ruleChecker.symPlusCheck(topDropCard, card))
                {
                    if (card.symbol == "plus4")
                    {
                        cardToPull += 4;
                    }
                    else if (card.symbol == "plus2")
                    {
                        cardToPull += 2;
                    }
                    

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
        /// Aktuális játékos lekérése
        /// </summary>
        /// <returns></returns>
        public Player currentPlayer()
        {
            return players[currentPlayerIndex];
        }

        /// <summary>
        /// Lépés a következő játékosra
        /// </summary>
        public Player nextPlayer()
        {
            if (clockWise == true)
            {
                if (currentPlayerIndex < players.Count() - 1)
                {
                    currentPlayerIndex++;
                }
                else
                {
                    currentPlayerIndex = 0;
                }

            }
            else
            {
                if (currentPlayerIndex > 0)
                {
                    currentPlayerIndex--;
                }
                else
                {
                    currentPlayerIndex = players.Count() - 1;
                }
            }

            return players[currentPlayerIndex];
        }

        public int currentPlayerIndex
        {
            set { this._currentPlayerIndex = value; }
            get { return this._currentPlayerIndex; }
        }

        public bool clockWise
        {
            set { this._clockWise = value; }
            get { return this._clockWise; }
        }

        public int cardToPull
        {
            set { this._cardToPull = value; }
            get { return this._cardToPull; }
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

        public void setNewColor(string color)
        {
            dropCards.topCard().color = color.ToUpperInvariant();
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
