using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Projektas
{
    internal class GeneralMethods
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;


        public GeneralMethods(IWebDriver driver)
        {
            this.driver = driver;
            wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        }


        public void ClickElementById(string id)
        {
            IWebElement el = wait.Until(x => x.FindElement(By.Id(id)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", el);
            el.Click();
        }

        public void EnterTextByXpath(string xpath, string text)
        {
            By searchField = By.XPath(xpath);
            driver.FindElement(searchField).SendKeys(text);
        }

        public void ItemListCheckByString(string xpath, string text)
        {

            IList<IWebElement> items = driver.FindElements(By.XPath(xpath));
            foreach (IWebElement item in items)
            {
                string itemText = item.Text.ToLower();
                string searchTextLower = text.ToLower();
                if (!itemText.Contains(searchTextLower))
                {
                    Assert.Fail("Not all search results contains the word " + text);
                }
            }
        }

        public void TitleCheck(string keyword)
        {

            string actualTitle = driver.Title;
            string expectedTitle = keyword;

            if (!actualTitle.Contains(expectedTitle))
            {
                Assert.Fail("Wrong title, missing" + expectedTitle);
            }
        }

        public void ScrollAndClickElementByJS(string xpath)
        {
            IWebElement el = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", el);
            js.ExecuteScript("arguments[0].click();", el);

        }

        public void ClickElementByJS(string xpath)
        {
            IWebElement el = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", el);

        }

        public static void CaptureScreenShot(IWebDriver driver, string fileName)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            if (!Directory.Exists("Screenshots"))
            {
                Directory.CreateDirectory("Screenshots");
            }

            screenshot.SaveAsFile(
                $"Screenshots\\{fileName}.png",
                ScreenshotImageFormat.Png);
        }



        public void ElementExists(string xPath)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                var element = driver.FindElement(By.XPath(xPath));
                return element.Displayed && !string.IsNullOrEmpty(element.Text);
            });
        }

        public string FindAndConvertNumbers(string xPath)
        {
            string numberString = driver.FindElement(By.XPath(xPath)).Text;
            numberString = numberString.Replace(",", ".");
            return numberString;
        }

        public IWebElement WaitForElement(string locator, int timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Timeout = TimeSpan.FromSeconds(timeoutInSeconds);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            return wait.Until(driver => driver.FindElement(By.XPath(locator)));
        }

       
    }

}

