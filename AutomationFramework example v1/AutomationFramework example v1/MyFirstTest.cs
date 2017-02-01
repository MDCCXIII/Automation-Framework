﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AutomationFramework_example_v1
{
    class MyFirstTest
    {
        public static IWebDriver driver = new ChromeDriver();
        static void Main(string[] args)
        {


            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
            driver.Manage().Window.Maximize();
            driver.Url = "https://aarp-test.kanacloud.com/GTConnect/UnifiedAcceptor/AARPDesktop.Main";
            IWebElement Text_UserName = new Elements().ById("userName");
            Text_UserName.SendKeys("ALP_MRA_15");
            IWebElement Text_Password = new Elements().ById("password");
            Text_Password.SendKeys("password1");
            IWebElement Button_Login = new Elements().ById("loginButton");
            Button_Login.Click();
            By btnContinue = By.Id("f5_btnContinue");
            if (driver.IsElementPresent(btnContinue))
            {
                IWebElement Button_Continue = new Elements().ById(btnContinue);
                Button_Continue.Click();
            }
            IWebElement Link_Actionable_Dashboard = new Elements().ByXpath("//{0}[contains({1}, '{2}')] ", new string[] { "a", "text()", "Handle Call" });
            Link_Actionable_Dashboard.Click();
            IWebElement PageHeader = new Elements().ByXpath("//div[contains(@id, 'tfhPerspectives')][contains(@style,'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//{0}[contains({1}, '{2}')]", new string[] { "h1", "@id", "lblIdentifyPerson" });
            if (!PageHeader.TextEqual("Identify Person")) { throw new Exception("Failed"); }
        }
    }

    public static class Extensions
    {
        public static bool IsElementPresent(this IWebDriver driver, By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, string Xpath, string[] parameters)
        {
            return driver.FindElement(By.XPath(string.Format(Xpath, parameters)));
        }

        public static IWebElement FindElement(this IWebDriver driver, string Xpath, string[] parameters, int timeoutInSeconds)
        {
            return driver.FindElement(By.XPath(string.Format(Xpath, parameters)), timeoutInSeconds);
        }

        public static void Wait(this IWebDriver driver, int timeoutInSeconds)
        {
            for (var i = 0; i < timeoutInSeconds; i++)
            {
                Thread.Sleep(1000);
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                try
                {
                    for (var i = 0; i < timeoutInSeconds; i++)
                    {
                        if (driver.IsElementPresent(by)) return driver.FindElement(by);
                        Thread.Sleep(1000);
                    }
                } catch (NoSuchElementException)
                {

                }
            }
            return driver.FindElement(by);
        }

        public static bool TextContains(this IWebElement element, string text)
        {
            bool result = false;
            if(element.Text.Contains(text))
            {
                result = true;
            }

            return result;
        }

        public static bool TextEqual(this IWebElement element, string text)
        {
            bool result = false;
            if (element.Text.Equals(text))
            {
                result = true;
            }

            return result;
        }

    }
        
}
