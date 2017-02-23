using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AutomationFramework_example_v1.Framework
{
    static class Driver
    {
        public static WebDriverWait wait = null;
        public static int serviceId = 0;
        public static DriverService service;
        public static IWebDriver driver;

        public static IWebDriver GetDriver(string BrowserName)
        {
            IWebDriver result = null;
            switch (BrowserName.ToLower())
            {
                case "chrome":
                    ChromeDriverService cService = ChromeDriverService.CreateDefaultService();
                    cService.Start();
                    serviceId = cService.ProcessId;
                    service = cService;
                    result = new ThreadLocal<IWebDriver>(() => { return new EventFiringWebDriver(new ChromeDriver()); }).Value;
                    break;
                case "ie" :
                    InternetExplorerDriverService ieService = InternetExplorerDriverService.CreateDefaultService();
                    ieService.Start();
                    serviceId = ieService.ProcessId;
                    service = ieService;
                    var options = new InternetExplorerOptions{ IgnoreZoomLevel = true };
                    result = new ThreadLocal<IWebDriver>(() => { return new EventFiringWebDriver(new InternetExplorerDriver(ieService, options)); } ).Value;
                    break;
                case "firefox":
                    FirefoxDriverService ffService = FirefoxDriverService.CreateDefaultService();
                    ffService.Start();
                    serviceId = ffService.ProcessId;
                    service = ffService;
                    result = new ThreadLocal<IWebDriver>(() => { return new EventFiringWebDriver(new FirefoxDriver(ffService)); }).Value;
                    break;
                default:
                    throw new Exception("Bad Driver Identifier: " + BrowserName + ".");
            }
            //result.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
            result.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
            result.Manage().Window.Maximize();
            
            wait = new WebDriverWait(result, new TimeSpan(0,0,30));
            driver = result;
            return result;
        }

        internal static void setUrl(this IWebDriver driver, ProjectInfo projectInfo)
        {
            driver.Url = projectInfo.Url;
        }
    }
}
