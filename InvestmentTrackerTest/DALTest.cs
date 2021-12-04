using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentTrackerTest
{
    [TestClass]
    public class DALTest : TestBase
    {
        [TestMethod]
        public void DatabaseAccessTest()
        {
            TestContext.WriteLine($"DB Location: {DataAccessLayer.DBDataSource}");
            TestContext.WriteLine("DB Connection String: " + DataAccessLayer.ConnectionString);

            DataAccessLayer DAL = new DataAccessLayer();
        }

        [TestMethod]
        public void ConnectToDB()
        {
            Test test = new Test();
            test.CreateDBFile();
            test.createTable();

            User user = new User()
            {
                FirstName = "Lucjan",
                Lastname = "De Spiegeleire"
            };

            test.AddUser(user);

            User recevedUser = test.GetUserById(1);
            Assert.AreEqual(recevedUser.FirstName, "Lucjan");
            Assert.AreEqual(recevedUser.Lastname, "De Spiegeleire");
        }
    }
}
