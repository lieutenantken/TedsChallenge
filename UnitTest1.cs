using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;


namespace TedsChallenge
{
    [TestClass]
    public class Tests
    {

        public TestContext testContext { get; set; }
        public YelpHomepage yelp = new YelpHomepage();
        private TestContext testContextInstance;
        private string pageUrl;
        String mssg; 
        Boolean first_third = false;
       
        
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        //[ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            //  PageFactory.InitElements(yelp.getPageDriver, yelp);  // depreciated
        }

        [TestInitialize]
        public void TestInitialize()
        {
       
        }


        [TestMethod]
        public void Z_Suggestions()
        {
            Boolean result = false;
            IList<IWebElement> multiElements;
            yelp.Suggestions();
            multiElements = yelp.getSuggestions;
            string elemname = "";



            foreach(IWebElement elem in multiElements)
            {
               elemname = elem.Text.ToLower();
               TestContext.WriteLine(elemname); // changed from Debug.WriteLine() 
               if (elemname.Contains("restaurants")) result = true;   
            }
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void A_searchTeds()
        {
            Boolean result = false;  

            yelp.homeLocators();
            pageUrl = yelp.enterFind();
            mssg = String.Format("pageUrl: {0}", pageUrl); 
            if(!pageUrl.Contains("empty")) result = true;
            else Debug.WriteLine(mssg);
            TestContext.WriteLine(mssg); 

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void B_firstAndThird()
        {   
           IList<IWebElement> multiElements;

           TestContext.WriteLine(" First & Third Test Begin ");
           if (pageUrl == null) A_searchTeds();

           yelp.LoadNewPage(pageUrl);
           yelp.searchLocators();
           if (yelp.findAllResults()) yelp.findFirstThird();
           mssg = String.Format(" The First All Result:  {0} ", yelp.getFirstReturnTitle);
           mssg += String.Format("\n The Third All Result:   {0} ", yelp.getThirdReturnTitle);
           TestContext.WriteLine(mssg);
                
           multiElements = yelp.getResults;
           mssg = String.Format("multiElements count: {0}", multiElements.Count.ToString());
           TestContext.WriteLine(mssg);

           first_third = true;
           Assert.IsTrue(first_third);
        }

        [TestMethod]
        public void C_TimeAgenda()
        {

            if (!first_third) B_firstAndThird();
            Thread.Sleep(1000);
            yelp.getclickFirstResult.Click();
            Debug.WriteLine("Clicked on First Result");
            Thread.Sleep(1000);

            yelp.timeAgenda();
            mssg = String.Format("Open Time: {0}", yelp.getopentime);
            TestContext.WriteLine(mssg);
            mssg = String.Format("Closing Time: {0}", yelp.getclosetime);
            TestContext.WriteLine(mssg);

            TestContext.WriteLine(yelp.getfoodDecision); 

        }


        [TestCleanup]
        public void RunAfterTests()
        {
           yelp.closeYelp();
        }

        [ClassCleanup]
        public static void shutdown()
        {
        }

     
    }

    public class TestRunner 
    {
        IWebDriver driver;

        public void DriverStart() 
        {
          driver = new ChromeDriver();
        }

        public IWebDriver getDriver
        {
            get
            {
                return this.driver;
            }
        }

        public void BrowserNavigate()
        {
            driver.Navigate().GoToUrl("http://yelp.com");
        }

        public void BrowserNavigate(string gotoPage)
        {
            driver.Navigate().GoToUrl(gotoPage);
        }

        public void CloseBrowser() 
        {
            driver.Close();
            driver.Quit();
        }
    
    }



}
