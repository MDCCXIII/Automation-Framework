using AutomationFramework_example_v1.Framework.Log.ExampleLogger;
using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace AutomationFramework_example_v1.Framework
{
    static class Elements
    {
        public static IWebElement ById(string id)
        {
            return Find(By.Id(id));
        }
        public static IWebElement ById(By id)
        {
            return Find(id);
        }

        public static IWebElement ByXpath(By Xpath)
        {
            return Find(Xpath);
        }
        public static IWebElement ByXpath(string Xpath)
        {
            return Find(By.XPath(Xpath));
        }
        public static IWebElement ByXpath(string Xpath, string[] parameters)
        {
            return ByXpath(string.Format(Xpath, parameters));
        }

        private static IWebElement Find(By by)
        {
            IWebElement result = null;
            int attempt = 0;
            while (attempt < 4)
            {
                attempt++;
                try
                {
                    if(Controller.driver.IsElementPresent(by)){
                        result = Driver.wait.Until(d => Controller.driver.FindElement(by));
                        break;
                    }
                    
                }
                catch (Exception ex)
                {
                    if (attempt >= 3)
                        ConsoleLogger.Log(ex);
                    break;
                }
                Thread.Sleep(3000);
            }
            return result;
        }

        public static IWebElement GetElement(ControlInfo controlInfo, XpathInfo pathInfo)
        {
            IWebElement result = null;
            switch (controlInfo.pathType.ToLower())
            {
                case "css":
                    break;
                case "xpath":
                    if (controlInfo.xpathNodeType != "" && controlInfo.xpathParameterName != "" && controlInfo.xpathParameterValue != "")
                    {
                        result = ByXpath(pathInfo.path, new string[] { controlInfo.xpathNodeType, controlInfo.xpathParameterName, controlInfo.xpathParameterValue });
                    }
                    else
                    {
                        result = ByXpath(pathInfo.path);
                    }
                    break;
                case "id":
                    result = ById(controlInfo.controlId);
                    break;
                default:
                    throw new Exception("The path type " + controlInfo.pathType + " is not a valid path type.");
            }

            return result;
        }
        public static By GetIdentifier(ControlInfo controlInfo, XpathInfo pathInfo)
        {
            By result = null;
            switch (controlInfo.pathType.ToLower())
            {
                case "css":
                    result = By.CssSelector(controlInfo.controlCss);
                    break;
                case "xpath":
                    if (controlInfo.xpathNodeType != "" && controlInfo.xpathParameterName != "" && controlInfo.xpathParameterValue != "")
                    {
                        result = By.XPath(string.Format(pathInfo.path, new string[] { controlInfo.xpathNodeType, controlInfo.xpathParameterName, controlInfo.xpathParameterValue }));
                    }
                    else
                    {
                        result = By.XPath(pathInfo.path);
                    }
                    break;
                case "id":
                    result = By.Id(controlInfo.controlId);
                    break;
                default:
                    throw new Exception("The path type " + controlInfo.pathType + " is not a valid path type.");
            }

            return result;
        }
    }
}
