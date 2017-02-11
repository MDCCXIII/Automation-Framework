using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
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
                    result = new ChromeDriver();
                    break;
                case "ie" :
                    var options = new InternetExplorerOptions{ IgnoreZoomLevel = true };
                    result = new InternetExplorerDriver(options);
                    break;
                case "firefox":
                    result = new FirefoxDriver();
                    break;
                default:
                    throw new Exception("Bad Driver Identifier: " + BrowserName + ".");
            }
            result.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            result.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(30));
            result.Manage().Window.Maximize();

            return result;
        }

        internal static void setUrl(this IWebDriver driver, string projectName)
        {
            ProjectInfo projectInfo = new ProjectInfo().Populate(projectName);
            driver.Url = projectInfo.Url;
        }
    }
}
