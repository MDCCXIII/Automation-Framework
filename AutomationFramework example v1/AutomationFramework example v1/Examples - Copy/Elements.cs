using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace AutomationFramework_example_v1
{
    class Elements
    {
        public IWebElement ById(string id)
        {
            return MyFirstTest.driver.FindElement(By.Id(id));
        }
        public IWebElement ById(string id, int timeoutInSeconds)
        {
            return MyFirstTest.driver.FindElement(By.Id(id), timeoutInSeconds);
        }
        public IWebElement ById(By id)
        {
            return MyFirstTest.driver.FindElement(id);
        }
        public IWebElement ById(By id, int timeoutInSeconds)
        {
            return MyFirstTest.driver.FindElement(id, timeoutInSeconds);
        }

        public IWebElement ByXpath(string Xpath)
        {
            return MyFirstTest.driver.FindElement(By.XPath(Xpath));
        }
        public IWebElement ByXpath(string Xpath, string[] parameters)
        {
            return MyFirstTest.driver.FindElement(Xpath, parameters);
        }
        public IWebElement ByXpath(string Xpath, int timeoutInSeconds)
        {
            return MyFirstTest.driver.FindElement(By.XPath(Xpath), timeoutInSeconds);
        }
        public IWebElement ByXpath(string Xpath, string[] parameters, int timeoutInSeconds)
        {
            return MyFirstTest.driver.FindElement(Xpath, parameters, timeoutInSeconds);
        }
    }
}
