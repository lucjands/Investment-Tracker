using Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvestmentTrackerTest
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        [Ignore]
        [TestCategory("Model")]
        public void CryptocurrencyTestUpperCaseTicker()
        {
            clsCryptocurrencyM cryptocurr = new clsCryptocurrencyM();
            cryptocurr.AssetID = 4;
            cryptocurr.CryptoID = 1;
            cryptocurr.Name = "Bitcoin";
            cryptocurr.Ticker = "btc";

            //cryptocurr.Validate
        }
    }
}
