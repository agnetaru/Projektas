using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projektas.POM
{
    public class TopMenu
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        string SearchFieldXpath = "//input[@type='search']";
        string SearchButtonXpath = "//button[@type='submit']";


        public TopMenu(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        public void SearchByText (string text)
        {
            generalMethods.EnterTextByXpath(SearchFieldXpath, text);

            generalMethods.ClickElementByJS(SearchButtonXpath);
        }

        
    }
}
