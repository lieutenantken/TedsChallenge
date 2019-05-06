using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TedsChallenge
{
   // [TestClass]
    public class UnitTest2
    {
        IWebDriver driver;

    //    [ClassInitialize]
        public void TestInitialize()
        {
           //   driver = new ChromeDriver();
        }

     //   [TestMethod]
        public void TestMethod1(TestContext testContext)
        {
         //  driver.Navigate().GoToUrl("http://yelp.com");
        }
    }
}
