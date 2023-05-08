using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


namespace Projektas.POM
{
    internal class ProductList
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        By searchResultsCheckXpath = By.XPath("//div[contains(@class,'bnProductCard__top')]//h3");
        By firstItemXpath = By.XPath("(//a[@class = 'bnProductCard__title'])[1]");
        By moreButtonXpath =By.XPath("//a[normalize-space()='Daugiau (9)']");
        By groupingXath = By.XPath("(//div[@class = 'customSelectText'])[1]");
        By listXpath = By.XPath("//div[contains(text(), 'Visas sąrašas')]");
        By sortingXpath = By.XPath("(//div[@class = 'customSelectText'])[2]");
        By ascendingOrderXpath = By.XPath("//div[contains(text(), 'Pigiausia viršuje')]");
        By priceXpath = By.XPath("//span[@class='price ']//span[@class='money_amount'] | //span[@class='price price--new']//span[@class='money_amount']");



        public ProductList(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }
        public void SearchResultsCheck(string text)
        {
            generalMethods.ItemListCheckByString(searchResultsCheckXpath, text);
        }

        public void CategoryFilterSelection(string subcategoryName)
        {
            generalMethods.ScrollAndClickElementByJS(moreButtonXpath);
            By subcategoryButtonXPath = By.XPath("//span[contains(text(),'" + subcategoryName + "')]");
            generalMethods.ScrollAndClickElementByJS(subcategoryButtonXPath);
        }

        public void SelectFirstItem()
        {
            generalMethods.WaitForElement(firstItemXpath, 7);
            generalMethods.ClickElementByJS(firstItemXpath);
        }

        public void Sorting()
        {
            generalMethods.ClickElementByJS(groupingXath);
            generalMethods.ClickElementByJS(listXpath);
            // Jei per greitai paspaudžia - puslapis užfreezina
            Thread.Sleep(1000);
            generalMethods.ClickElementByJS(sortingXpath);
            generalMethods.ClickElementByJS(ascendingOrderXpath);
        }

       
        public void ListSortingByPriceAsc()
        {
            var priceElements = driver.FindElements(priceXpath);
            var prices = priceElements.Select(element => double.Parse(element.Text.Replace(",", "."))).ToList();

            var sortedPrices = new List<double>(prices);
            sortedPrices.Sort();

            foreach (var price in prices)
            {
                Console.WriteLine(price);
            }

            bool isSorted = prices.SequenceEqual(sortedPrices);

            Console.WriteLine("The prices are sorted in ascending order: " + isSorted);

        }
    }
}