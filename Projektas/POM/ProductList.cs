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

        string searchResultsCheckXpath = "//div[contains(@class,'bnProductCard__top')]//h3";
        string firstItemXpath = "(//a[@class = 'bnProductCard__title'])[1]";
        string moreButtonXpath = "//a[normalize-space()='Daugiau (9)']";
        string groupingXath = "(//div[@class = 'customSelectText'])[1]";
        string listXpath = "//div[contains(text(), 'Visas sąrašas')]";
        string sortingXpath = "(//div[@class = 'customSelectText'])[2]";
        string ascendingOrderXpath = "//div[contains(text(), 'Pigiausia viršuje')]";




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
            string subcategoryButtonXPath = "//span[contains(text(),'" + subcategoryName + "')]";
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
            var priceElements = driver.FindElements(By.XPath("//span[@class='price ']//span[@class='money_amount'] | //span[@class='price price--new']//span[@class='money_amount']"));
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