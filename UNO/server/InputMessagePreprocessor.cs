using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using game;
using System.Threading;

namespace server
{
    class InputMessagePreprocessor
    {
        Message message;
        List<Message> messageList = null;
        public InputMessagePreprocessor(Message message, Player player)
        {
            this.message = message;
            

            #region >>> Bejött üzenet feldolgozása <<<
            #region ### Kártya ###
            if (message.head.STATUS.Equals("CARD"))
            {
                if (message.head.STATUSCODE.Equals("CARD"))
                {
                    if (game.dropCard(player, message.body.CARD))
                    {
                        this.message = new Message("MSG", "SERVER", player.username, "Card droped");
                    }
                    else
                    {
                        this.message = new Message("ERROR", "SERVER", player.username, "You can not place that card");
                    }
                }
                else if (message.head.STATUSCODE.Equals("UNO"))
                {
                    this.message = new Message(
                        "MSG", "SERVER", player.username, "This function is not ready yet"
                    );
                }
            }
            #endregion
            
            #region ### Kliens oldali hiba ###
            else if (message.head.STATUS.Equals("ERROR"))
            {
                this.message = message;
            }
            #endregion
            #region ### Parancs ###
            else if (message.head.STATUS.Equals("COMMAND") && !message.head.STATUSCODE.Equals("UNDEFINED"))
            {
                if (message.head.STATUSCODE.Equals("HAND"))
                {
                    this.messageList = new List<Message>();
                    foreach (Card card in player.getCardList())
                    {
                        this.messageList.Add(new Message("CARD", "SERVER", player.username, card));
                    }
                }
                else if (message.head.STATUSCODE.Equals("TOP"))
                {
                    this.message = new Message("CARD", "SERVER", player.username, game.topDroppedCard());
                }
                else if (message.head.STATUSCODE.Equals("DRAW"))
                {
                    game.pullCard(player);
                    this.message = new Message("MSG", "SERVER", player.username, "Card added"), player);
                }
            }
            # endregion
            #region ### Segítség ###
            else if (message.head.STATUS.Equals("HELP"))
            {
                if (message.head.STATUSCODE.Equals("COMMAND"))
                {
                    this.message = new Message("MSG", "SERVER", player.username, new Help().Commands());
                }
            }
            #endregion
            #endregion

        }

        public InputMessagePreprocessor()
        {

        }

        public Message Response(){
            return message;
        }

        public List<Message> ResponseL()
        {
            return messageList;
        }

        public Message ConvertToMessage (string inpuString, Card card = null)
        {
            Message msg;
            if (card != null){
                

                // üzenet rész
            }
            else
            {
                //Kérdéses, hogy kell-e ide, ha valahol kártyával együtt akarok üzenetet küldeni
                //de nem tudom, hogy erre van-e szükség egyáltalán
                //

            }
            return null;
        }

    }
}
