using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class clsTransactionPointM
    {
        public int TransactionPointID { get; set; }

        public int AssetID { get; set; }

        public BuySell BuySell { get; set; }

        public double Amount { get; set; }

        public DateTime Timestamp { get; set; }

        public string Description { get; set; }
    }
}
