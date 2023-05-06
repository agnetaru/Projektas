using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V108.CSS;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Threading;
using Projektas.POM;
using System.IO;
using NUnit.Framework.Interfaces;

namespace Projektas
{
    public class Class1


    {
        static IWebDriver driver;

        TopMenu topMenu;
        Navigation navigation;
        ProductList productList;
        Login login;
        Cart cart;
        static GeneralMethods generalMethods;

        public static string searchForItem = "vichy";
        public static string checkResultsByText = "vichy";
        public static string filterSelection = "Odos priežiūra ir kosmetika";
        public static string categorySelector = "Vitaminai ir mineralai";
        public static string subcategorySelector = "Imunitetui";




        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.benu.lt/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(7);

            generalMethods = new GeneralMethods(driver);
            topMenu = new TopMenu(driver);
            navigation = new Navigation(driver);
            productList = new ProductList(driver);
            cart = new Cart(driver);
            login = new Login(driver);
            generalMethods.ClickElementById("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");

        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var name =
                    $"{TestContext.CurrentContext.Test.MethodName}" +
                    $" Error at " +
                    $"{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'_'mm'_'ss")}";

                GeneralMethods.CaptureScreenShot(driver, name);

                File.WriteAllText(
                    $"Screenshots\\{name}.txt",
                    TestContext.CurrentContext.Result.Message);
            }
            driver.Close();
        }

 
        [Test]

        public void Search()
        {

            
            topMenu.SearchByText(searchForItem);
            navigation.PageNameCheck(searchForItem);
            productList.SearchResultsCheck(checkResultsByText);
            productList.CategoryFilterSelection(filterSelection);
            productList.SelectFirstItem();
            navigation.BreadcrumbsCheck(filterSelection);
           
        }

        [Test]

        public void ProductListSorting()
        {
            navigation.HoverOverCategory(categorySelector, subcategorySelector);
            productList.Sorting();
            //WaitForElement ir ElementExists metodai nepadeda, be thread sleep neranda kainų
            Thread.Sleep(2000);
            productList.ListSortingByPriceAsc();
            

        }

        [Test]

        public void Cart()
        {
            topMenu.SearchByText(searchForItem);
            productList.SelectFirstItem();
            cart.AddItemToCartAndCheckPrice();
            cart.GoToCart();
            cart.AddItemAndCheckTotalPrice();
            cart.EmptyTheCart();
            cart.CheckIfCartIsEmpty();
        }

        [Test]

        public void Login()
        {
            navigation.LoginPage();
            login.EnterFalseEmailAndPassword();
            login.AssertLoginFailed();

        }
    }
}
