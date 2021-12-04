using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class clsPricePointM
    {
        public int PricePointID { get; set; }

        public int AssetID { get; set; }

        public double Price { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
