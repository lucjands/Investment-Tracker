using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Investment_Tracker.Model
{
    class Cryptocurrency
    {
        public int AssetID { get; set; }

        public int CryptoID { get; set; }

        public string Name { get; set; }

        public string Ticker { get; set; }
    }
}
