using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;

namespace AutomationFramework_example_v1.Framework
{
    static class Elements
    {
        public static IWebElement ById(string id)
        {
            return Controller.driver.FindElement(By.Id(id));
        }
        public static IWebElement ById(string id, int timeoutInSeconds)
        {
            return Controller.driver.FindElement(By.Id(id), timeoutInSeconds);
        }
        public static IWebElement ById(By id)
        {
            return Controller.driver.FindElement(id);
        }
        public static IWebElement ById(By id, int timeoutInSeconds)
        {
            return Controller.driver.FindElement(id, timeoutInSeconds);
        }

        public static IWebElement ByXpath(By Xpath)
        {
            return Controller.driver.FindElement(Xpath);
        }
        public static IWebElement ByXpath(string Xpath)
        {
            return Controller.driver.FindElement(By.XPath(Xpath));
        }
        public static IWebElement ByXpath(string Xpath, string[] parameters)
        {
            return Controller.driver.FindElement(Xpath, parameters);
        }
        public static IWebElement ByXpath(string Xpath, int timeoutInSeconds)
        {
            return Controller.driver.FindElement(By.XPath(Xpath), timeoutInSeconds);
        }
        public static IWebElement ByXpath(string Xpath, string[] parameters, int timeoutInSeconds)
        {
            return Controller.driver.FindElement(Xpath, parameters, timeoutInSeconds);
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
