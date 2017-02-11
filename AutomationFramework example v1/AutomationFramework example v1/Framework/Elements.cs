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

        public static IWebElement GetElement(ControlInfo controlInfo, PathInfo pathInfo)
        {
            IWebElement result = null;
            if (pathInfo != null)
            {
                result = NewMethod(pathInfo.pathType, controlInfo, pathInfo, result);
            }
            else
            {
                if(controlInfo.identifyingNodeType != null)
                {
                    result = NewMethod(controlInfo.identifyingNodeType, controlInfo, pathInfo, result);
                }
                else
                {
                    throw new Exception("No path type provided for the control " + controlInfo.controlName + ".");
                }
            }
            

            return result;
        }

        private static IWebElement NewMethod(string pathType, ControlInfo controlInfo, PathInfo pathInfo, IWebElement result)
        {
            switch (pathType.ToLower())
            {
                case "css":
                    break;
                case "xpath":
                    if (controlInfo.identifyingNodeType != "" && controlInfo.identifyingParameterName != "" && controlInfo.identifyingParameterValue != "")
                    {
                        result = ByXpath(pathInfo.path, new string[] { controlInfo.identifyingNodeType, controlInfo.identifyingParameterName, controlInfo.identifyingParameterValue });
                    }
                    else
                    {
                        result = ByXpath(pathInfo.path);
                    }
                    break;
                case "id":
                    result = ById(controlInfo.identifyingParameterValue);
                    break;
                default:
                    throw new Exception("The path type " + pathInfo.pathType + " is not a valid path type.");
            }

            return result;
        }
    }
}
