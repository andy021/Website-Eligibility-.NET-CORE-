using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    public class SearchWebsite
    {
        [TestMethod]
        public void searchWebsite()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),chromeOptions)) 
            {
                driver.Navigate().GoToUrl("http://google.com");
                IWebElement query = driver.FindElement(By.Name("q"));
                query.SendKeys("cheese");
                query.Submit();
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.StartsWith("cheese", StringComparison.OrdinalIgnoreCase));
                Assert.AreEqual(driver.Title, "cheese - Google Search");
            }
        }
    }
}
