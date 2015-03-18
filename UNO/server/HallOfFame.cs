using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    class HallOfFame
    {
        private List<Record> records;

        HallOfFame() { 
            records =new List<Record>();
        }
        ///<summary></summary>
        /// <param name="records">Az eltárolt bejegyzések</param>

        public void addRecord(Record record)
        {
            records.Add(record);
        }

        public List<Record> getResults()
        {
            return records;
        }


        //most nem írok remove-ot, mert eléletileg nem kell, talán majd extraként
        // egyébként is: THE LEGEND NEVER DIES!
    }
}
