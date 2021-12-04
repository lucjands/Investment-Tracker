using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentTrackerTest
{
    public class TestBase
    {
        public TestContext TestContext { get; set; }
        protected string _GoodFileName;

        protected void WriteDescription(Type typ)
        {
            string testName = TestContext.TestName;

            //Find the test method currently executing
            MemberInfo method = typ.GetMethod(testName);
            if (method != null)
            {
                // See if the [Description] attr exists on this test
                Attribute attr = method.GetCustomAttribute(typeof(DescriptionAttribute));
                if (attr != null)
                {
                    // Cast the attr to a DescriptionAttr
                    DescriptionAttribute dattr = (DescriptionAttribute)attr;
                    // Display the test description
                    TestContext.WriteLine("Test Description: " + dattr.Description);
                }
            }
        }
    }
}
