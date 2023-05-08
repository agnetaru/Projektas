using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projektas.POM
{
    internal class Cart
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        By checkCartXpath = By.XPath("//a[@class = 'button button--green viewCart']");
        By priceOfAnItem = By.XPath("//span[@class='pric']//span[@class='money_amount'][1]");
        By addToCartButton = By.XPath("//button[contains(@class, 'addToCart')]");
        By popUpCartXpath = By.XPath("//div[@class='modal-content']");
        By priceInThePopUpCart = By.XPath("(//ins[@class='productItem__priceRegular']//span[@class='money_amount'])[1]");
        By itemPriceXpath = By.XPath("//span[@class='cartTable__priceValue']//span[@class='money_amount']");
        By addOneMoreToTheCart = By.XPath("//i[@class='fal fa-plus']");
        By totalAmountXpath = By.XPath("//span[@class='cartTable__subtotalValue']//span[@class='money_amount']");
        By emptyCartXpath = By.XPath("(//i[@class='fal fa-trash-alt'])[1]");
        By cartIsEmptyNotification = By.XPath("//div[@class='cart_cartIsEmpty']");

        public Cart(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

       
        public void AddItemToCartAndCheckPrice()
        {   
            string itemPrice = generalMethods.FindAndConvertNumbers(priceOfAnItem);
           
            generalMethods.ClickElementByJS(addToCartButton);
            
            var cartWindow = generalMethods.WaitForElement(popUpCartXpath, 10);
           
            var cartWindowHandle = driver.WindowHandles.Last();
            driver.SwitchTo().Window(cartWindowHandle);
           
            generalMethods.ElementExists(priceInThePopUpCart);
            var cartItemPrice = generalMethods.FindAndConvertNumbers(priceInThePopUpCart);

            Assert.AreEqual(itemPrice, cartItemPrice, "Item price in cart window does not match the price on the product page");

            Console.WriteLine("Item price: " + itemPrice + " Item price in the cart: " + cartItemPrice);
        }

       public void GoToCart()
        {
            generalMethods.ClickElementByJS(checkCartXpath);
        }

        public void AddItemAndCheckTotalPrice()
        {
            var itemPrice = generalMethods.FindAndConvertNumbers(itemPriceXpath);
            double originalPrice = double.Parse(itemPrice);

            generalMethods.ClickElementByJS(addOneMoreToTheCart);

            // Thread Sleep'as nes neturiu daugiau už ko užsikabinti, nes elementas visą laiką egzistuoja, tekstas jame taip pat, tad belieka laukti kol persikraus
            Thread.Sleep(3000);

            var totalPrice = generalMethods.FindAndConvertNumbers(totalAmountXpath);
            double expectedTotalPrice = originalPrice * 2;

            Assert.AreEqual(expectedTotalPrice, double.Parse(totalPrice), 0.01, "Total price is incorrect");

            Console.WriteLine("Expected price: " + expectedTotalPrice + " actual price: " + totalPrice);
        }

        public void EmptyTheCart()
        {
            generalMethods.ClickElementByJS(emptyCartXpath);
        }

        public void CheckIfCartIsEmpty()
        {
            generalMethods.ElementExists(cartIsEmptyNotification);
        }
    }
}
