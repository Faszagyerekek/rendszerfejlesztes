using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;
using System.Net.Sockets;

namespace server
{
    class PlayRoom
    {
        Message message;
        Player player;
        List<Player> playerList;
        Game game;

        public PlayRoom(List<Player> players)
        {
            this.playerList = players;
            gamePlay();
        }

        public void Preprocessor(Message message)
        {

        }


        private void gamePlay()
        {
            game = new Game(playerList);
            game.cardDealing();
        }

        private Player Identify(TcpClient client)
        {
            foreach (Player player in playerList)
            {
                if (client.Client.Handle.ToInt32() == player.ID)
                {
                    return player;
                }
            }
            return null;
        }
    }
}
