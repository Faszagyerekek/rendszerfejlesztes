using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using game;
namespace server
{
    class Record
    {
        private string _username;
        private int _points;

        public Record() { }

        /// <summary>
        /// Felüldefiniált konstruktor (felhasználónév, pontok)
        /// </summary>
        /// <param name="username">A játékos felhasználó neve</param>
        /// <param name="points">A játékos pontjai</param>

        public string username
        {
            set { this._username = value; }
            get { return this._username; }
        }
        
        public int points
        {
            set { this._points = value; }
            get { return this._points; }
        }

        public Record(string username, int points)
        {
            this.username = username;
            this.points = points;
        }


    }
}
