using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projektas.POM
{
    internal class Login
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        By emailXpath = By.XPath("//input[@id='email']");
        By passwordXpath = By.XPath("//input[@id='password']");
        By submitButton = By.XPath("//button[@type='submit']");
        By errorBox = By.XPath("//div[@class='errorsBox']");


        public Login(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

       
        public void EnterFalseEmailAndPassword()
        {
            string[] lines = System.IO.File.ReadAllLines(@"LogIn.txt");
            string email = lines[0];
            string password = lines[1];
            generalMethods.EnterTextByXpath(emailXpath, email);
            generalMethods.EnterTextByXpath(passwordXpath, password);
            generalMethods.ClickElementByJS(submitButton);
        }

        public void AssertLoginFailed()
        {
            bool isElementPresent = driver.FindElements(errorBox).Count > 0;
            Assert.IsTrue(isElementPresent, "Login failed message not found");
        }
    }
}
