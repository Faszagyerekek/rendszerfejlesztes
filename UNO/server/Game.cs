﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using server;

namespace game
{
    class Game
    {
        private List<Player> players;
        private DeckStorageAncestor dropCards;
        private DeckStorageAncestor pullCards;
        private int sameDropCards;

        public Game(List<Player> players)
        {
            this.players = players;
            this.dropCards = new DeckStorageAncestor();
            this.pullCards = new DeckStorageAncestor();
            this.sameDropCards = 1;
        }

        /*
         * Kártyalapok betöltése a húzópakliba
        */
        public void loadDeck() {
            pullCards = new DataLinkLayer().loadDeck();
        }

        /*
         * Osztás: Minden játékos kap 8 véletlen lapot a húzópakliból
        */
        public void cardDealing() {
            foreach (Player player in players) {
                for (int i = 0; i < 8; i++) {
                    player.addCard(pullCards.deal());
                }
            }
        }

        /*
         * A dobópakli legfelső lapjának megadása
        */
        public Card topCard()
        {
            return dropCards.topCard();
        }

        /*
         * Kártya húzása: Játékos húz egy véletlen lapot a húzópakliból
        */
        public void pullCard(Player player)
        {
            player.addCard(pullCards.deal());
        }

        /*
         * Kártya dobása: Játékos eldob egy lapot, ami a dobópakliba kerül
        */
        public void dropCard(Player player, Card card)
        {
            Card topDropCard = topCard();
            if (card.symEquals(topDropCard))
            {
                sameDropCards++;
            }
            else
            {
                sameDropCards = 1;
            }

            player.dropCard(card);
            dropCards.addCard(card);
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