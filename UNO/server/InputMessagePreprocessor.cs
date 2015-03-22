using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using game;

namespace server
{
    class InputMessagePreprocessor
    {
        Message message;
        public InputMessagePreprocessor(Message message)
        {
            this.message = message;
        }

        public InputMessagePreprocessor()
        {

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
