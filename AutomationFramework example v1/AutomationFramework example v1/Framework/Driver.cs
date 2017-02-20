using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using System;

namespace AutomationFramework_example_v1.Framework
{
    static class Driver
    {
        public static IWebDriver GetDriver(string BrowserName)
        {
            IWebDriver result = null;
            switch (BrowserName.ToLower())
            {
                case "chrome":
                    result = new EventFiringWebDriver(new ChromeDriver());
                    break;
                case "ie" :
                    var options = new InternetExplorerOptions{ IgnoreZoomLevel = true };
                    result = new EventFiringWebDriver(new InternetExplorerDriver(options));
                    break;
                case "firefox":
                    result = new EventFiringWebDriver(new FirefoxDriver());
                    break;
                default:
                    throw new Exception("Bad Driver Identifier: " + BrowserName + ".");
            }
            result.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
            result.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
            result.Manage().Window.Maximize();

            return result;
        }

        internal static void setUrl(this IWebDriver driver, ProjectInfo projectInfo)
        {
            driver.Url = projectInfo.Url;
        }
    }
}
