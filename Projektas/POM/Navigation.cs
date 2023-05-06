using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projektas.POM
{
    internal class Navigation
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        string breadcrumbsXpath = "//div[@id='breadcrumbsWrap']";
        string accountIcon = "//li[@class='accountIcon']//a";
        string loginPage = "//h2[normalize-space()='Sveiki']";



        public Navigation(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }
        public void PageNameCheck(string text)
        {
            generalMethods.TitleCheck(text);
        }

        public void BreadcrumbsCheck(string text)
        {
            IWebElement breadcrumbs = driver.FindElement(By.XPath(breadcrumbsXpath));
            if (!breadcrumbs.Text.Contains(text))
            {
                Assert.Fail("Breacrumbs doesn't contain " + text);
            }
        }

        public void HoverOverCategory(string categoryName, string subCategoryName)
        {
            IWebElement category = driver.FindElement(By.XPath("//a[contains(text(),'"+categoryName+"')]"));
            Actions action = new Actions(driver);
            action.MoveToElement(category).Perform();
            Thread.Sleep(1000);
            generalMethods.ClickElementByJS("//a[normalize-space()='" + subCategoryName + "']");
      

        }
        public void LoginPage()
        {
            generalMethods.ClickElementByJS(accountIcon);
            generalMethods.ElementExists(loginPage);
        }
    }
}
