using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;


namespace TedsChallenge
{
    public class YelpHomepage
    {

        IList<IWebElement> suggestionList, resultsList; 
        public TestRunner runDriver = new TestRunner();
        IWebElement findBox, suggestionContainer, thirdResult;
        IWebElement nearBox, findField, inputFind, inputNear, searchSubmit;
        IWebElement resultsListContainer, allResults, suggestionsUL;
        IWebElement mapColumnTransitionDiv, firstResult, allresultsParent;
        IWebElement clickFirstResult;
        IWebElement shortDefList, uSpaceHalf;
        IWebDriver pageDriver;
        WebDriverWait wait;
        int childcount;
        string pageover, mssg, firstReturnTitle, thirdReturnTitle;
        string firstresult, thirdresult, opentime, closetime;
        string foodDecision;


        public YelpHomepage() 
        {
            runDriver.DriverStart();
            pageDriver = runDriver.getDriver;
            runDriver.BrowserNavigate();
            wait = new WebDriverWait(pageDriver, TimeSpan.FromSeconds(5));
            pageover = "empty url";
        }

        public IWebDriver getPageDriver
        {  get { return this.pageDriver; } }

        public void closeYelp() 
        {
            Thread.Sleep(2000);
            runDriver.CloseBrowser();
        }

       


 #region Locators
       public void homeLocators()
       {

       string suggestionclass = "pseudo-input_wrapper";
       By suggestionBy = By.ClassName(suggestionclass);
       findBox = pageDriver.FindElement(suggestionBy);

       string suggestioncontainer = "[class*=search-suggestions-list-container]";
       By suggestionListContainerBy = By.CssSelector(suggestioncontainer);
       suggestionContainer = pageDriver.FindElement(suggestionListContainerBy);

       string suggestions = "ul[class=suggestions-list]";
       By suggestionListBy = By.CssSelector(suggestions);
       suggestionsUL = suggestionContainer.FindElement(suggestionListBy);

       string nearbox = "near-decorator";
       By nearClass = By.ClassName(nearbox);
       nearBox = pageDriver.FindElement(nearClass);

       string findfield = "pseudo-input_field-holder";
       By findfieldBy = By.ClassName(findfield);
       findField = findBox.FindElement(findfieldBy);

       string inputfind = "[id=find_desc]";
       By inputfindBy = By.CssSelector(inputfind);
       inputFind = findField.FindElement(inputfindBy);

       string nearfind = "[id=dropperText_Mast]";
       By nearfindBy = By.CssSelector(nearfind);
       inputNear = nearBox.FindElement(nearfindBy);

       string searchsubmit = "[id=header-search-submit]";
       By searchsubmitBy = By.CssSelector(searchsubmit);
       searchSubmit = pageDriver.FindElement(searchsubmitBy);

       }

       public void searchLocators() 
       {
        
       string mapColumnTransition = "//div[contains(@class,'mapColumnTransition')]";
       By mapColumnTransitionBy = By.XPath(mapColumnTransition);
       mapColumnTransitionDiv = pageDriver.FindElement(mapColumnTransitionBy);

       mssg = String.Format("suggestion (class): {0}", mapColumnTransitionDiv.GetAttribute("class"));
       Debug.WriteLine(mssg);

       string resultscontainer = "//ul[starts-with(@class,'lemon')]";
       By resultscontainerBy = By.XPath(resultscontainer);
       resultsListContainer = mapColumnTransitionDiv.FindElement(resultscontainerBy);

       mssg = String.Format("suggestion (class): {0}", resultsListContainer.GetAttribute("class"));
       Debug.WriteLine(mssg);

       string resultslist = "//li[starts-with(@class,'lemon')]";
       By resultslistBY = By.XPath(resultslist);
       resultsList = resultsListContainer.FindElements(resultslistBY);

       }

 #endregion Locators
        

 #region constructor
       public void Suggestions()
       {
           if (PageLoad())
           {
               homeLocators();
               selectFindOptions();
               By findList = By.TagName("li");
               suggestionList = suggestionsUL.FindElements(findList);
               mssg = String.Format("suggestionList (class): {0}", suggestionList[0].GetAttribute("class"));
               Debug.WriteLine(mssg);
           }
           else { Debug.WriteLine("HomePage failed to load");  }
       }

       public IList<IWebElement> getSuggestions
       { get{ return this.suggestionList;} }

       public IList<IWebElement> getResults
       { get { return this.resultsList; } }

       public string getFirstReturnTitle
       { get { return this.firstReturnTitle; } }

       public string getThirdReturnTitle
       { get { return this.thirdReturnTitle; } }  //clickFirstResult

       public IWebElement getclickFirstResult
       { get { return this.clickFirstResult; } }

       public string getopentime
       { get { return this.opentime; } }  

       public string getclosetime
       { get { return this.closetime; } }  

       public string getfoodDecision
       { get { return this.foodDecision; } } 


       public Boolean PageLoad()
       {
        bool loaded = false;
        loaded = new WebDriverWait(pageDriver, TimeSpan.FromSeconds(70)).Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));
        return loaded; 
       }

       public void LoadNewPage(string newPage)
       {
           mssg = String.Format("LoadNewPage : {0}", newPage);
           Debug.WriteLine(mssg);
           runDriver.BrowserNavigate(newPage);
       }

       public Boolean findAllResults()
       {

           Boolean allResultsFound = false;
           mssg = "allResults returned NULL!";
           childcount = 1;
           allResults = null;
           Debug.WriteLine("allResults search");
           string h3allresults = "";
           IWebElement h3allResults = null;

           foreach(IWebElement elem in resultsList)
            {

                string allresults = String.Format("//ul[starts-with(@class,'lemon')]/li[position()={0}]", childcount); 
                By allresultsBY = By.XPath(allresults);
                try
                    { 
                        allResults = resultsListContainer.FindElement(allresultsBY);
                        h3allresults = "div>div>h3";
                        By h3allresultsBY = By.CssSelector(h3allresults);
                        h3allResults = allResults.FindElement(h3allresultsBY);
                    }
                catch( NotFoundException ex)
                    {
                        mssg = String.Format("error caught: {0}", ex.GetType().FullName);
                      //  Debug.WriteLine(mssg);
                    }

                if ((allResults != null) && (h3allResults != null))
                {

                    mssg = String.Format("allresults (class): {0}", allResults.GetAttribute("class"));
                    mssg += String.Format("\n allresults (class): {0}", h3allResults.GetAttribute("class"));
                    mssg += String.Format(" \n text:: {0}", h3allResults.Text);
                    Debug.WriteLine(mssg);
                    if (allResults.Text.Contains("All Results")) { allResultsFound = true; break; }
                }

             childcount++;      
            }

          return allResultsFound;
       }


       public void findFirstThird()
       {

           mssg = "allResults returned NULL!";
           int firstCount = childcount + 1;
           int thirdCount = childcount + 3;

           IWebElement h3allResults = null;
           string h3allresults = "";

           string allresultsparent = ".//..";
           By allresultsparentBY = By.XPath(allresultsparent);
           allresultsParent = allResults.FindElement(allresultsparentBY);


            firstresult = String.Format("//ul[starts-with(@class,'lemon')]/li[position()={0}]", firstCount);
            By firstResultBY = By.XPath(firstresult);
            firstResult = resultsListContainer.FindElement(firstResultBY);
            h3allresults = "div>div>h3";
            By h3firstresultsBY = By.CssSelector(h3allresults);
            h3allResults = firstResult.FindElement(h3firstresultsBY);

            mssg = String.Format("firstResult (class): {0}", firstResult.GetAttribute("class"));
            mssg += String.Format(" \n text:: {0}", h3allResults.Text);
            Debug.WriteLine(mssg);

            firstReturnTitle = h3allResults.Text;
            By a_allResultsBY = By.CssSelector("a");
            clickFirstResult = h3allResults.FindElement(a_allResultsBY); 

            thirdresult = String.Format("//ul[starts-with(@class,'lemon')]/li[position()={0}]", thirdCount);
            By thirdResultBY = By.XPath(thirdresult);
            thirdResult = resultsListContainer.FindElement(thirdResultBY);
            h3allresults = "div>div>h3";
            By h3thirdresultsBY = By.CssSelector(h3allresults);
            h3allResults = thirdResult.FindElement(h3thirdresultsBY);

            mssg = String.Format("firstResult (class): {0}", firstResult.GetAttribute("class"));
            mssg += String.Format(" \n text:: {0}", h3allResults.Text);
            Debug.WriteLine(mssg);

            thirdReturnTitle = h3allResults.Text;

       }


       public void timeAgenda()
       {

           PageLoad();  //dl short-def-list class 
           IWebElement timeTopSpan, timeLowerSpan;
 

           string shortdeflist = "dl[class=short-def-list]";
           By shortdeflistBY = By.CssSelector(shortdeflist);
           shortDefList = pageDriver.FindElement(shortdeflistBY);
        
           string uspacehalf = "dd>[class=u-space-r-half]";
           By uspacehalfBY = By.CssSelector(uspacehalf);
           uSpaceHalf = shortDefList.FindElement(uspacehalfBY);

           //opentime, closetime; 
           string topspan = ".//span[@class='nowrap']";
           By topspanfBY = By.XPath(topspan);
           timeTopSpan = uSpaceHalf.FindElement(topspanfBY);

           string lowerspan = ".//span[@class='nowrap' and position()=2]";
           By lowerspanBY = By.XPath(lowerspan);
           timeLowerSpan = uSpaceHalf.FindElement(lowerspanBY);

           opentime = timeTopSpan.Text;
           closetime = timeLowerSpan.Text;

           FoodDecision(); 

       }
      


 #endregion constructor

       //All Results 

 #region Actions
       public string enterFind()
       {
           bool isloaded = false; 

           this.inputFind.Click();
           this.inputFind.SendKeys("Teds Montana Grill");
           this.inputNear.Click();
           this.inputNear.SendKeys("Denver, CO");
           this.searchSubmit.Click();
           isloaded = PageLoad();
           if (isloaded) pageover = pageDriver.Url;

           return pageover;
       }

       public void selectFindOptions()
       {
           this.findBox.Click();
       }

       private void FoodDecision() 
       {

           DateTime CurrentTime, OpenTime, ClosingTime, DinnerTime;
         
           string dinnerTime = "3:00 pm";
           Boolean timefound = false;


           CurrentTime = DateTime.Now;
           DinnerTime = Convert.ToDateTime(dinnerTime);  //dinnerTime
           OpenTime = CurrentTime; ClosingTime = CurrentTime;


           mssg = String.Format("The Current Time: {0}", CurrentTime.ToString("t"));
           Debug.WriteLine(mssg);

           if (opentime.ToLower().Contains("am"))
           {
               OpenTime = Convert.ToDateTime(opentime);   // opentime  timeconv[0]
               mssg = String.Format("The Open Time: {0}", OpenTime.ToString("t"));
               Debug.WriteLine(mssg);
           }

           if (closetime.ToLower().Contains("pm"))
           {
               ClosingTime = Convert.ToDateTime(closetime);   // opentime  timeconv[0]
               mssg = String.Format("The Close Time: {0}", ClosingTime.ToString("t"));
               Debug.WriteLine(mssg);
           }

           if (!timefound && CurrentTime.TimeOfDay < OpenTime.TimeOfDay)
           { foodDecision = String.Format(" Oh No, Ted's not open yet! "); timefound = true; }
           if (!timefound && CurrentTime.TimeOfDay > ClosingTime.TimeOfDay)
           { foodDecision = String.Format(" Oh No, Ted's closed up! "); timefound = true; }

           if (!timefound && CurrentTime.TimeOfDay < DinnerTime.TimeOfDay)
           { foodDecision = String.Format(" It is time to go to Ted’s for lunch! "); timefound = true; }
           if (!timefound && CurrentTime.TimeOfDay > DinnerTime.TimeOfDay)
           { foodDecision = String.Format("It is time to go to Ted’s for dinner!"); timefound = true; }

           if (timefound) { Debug.WriteLine("The time selection chose foodDecision! "); } else { Debug.WriteLine(" The time remains uncertain "); }
       }



 #endregion Actions

    }  // public class YelpHomepage 
}  // namespace TedsChallenge
