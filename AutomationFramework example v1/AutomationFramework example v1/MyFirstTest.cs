using OpenQA.Selenium;
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
            // Navagation to URL
            driver.Url = "https://aarp-test.kanacloud.com/GTConnect/UnifiedAcceptor/AARPDesktop.Main";
            // Login
            IWebElement Text_UserName = new Elements().ById("userName");
            Text_UserName.SendKeys("ALP_MRA_15");
            IWebElement Text_Password = new Elements().ById("password");
            Text_Password.SendKeys("password2");
            IWebElement Button_Login = new Elements().ById("loginButton");
            Button_Login.Click();
            By btnContinue = By.Id("f5_btnContinue");
            // Work around if another user is login
            if (driver.IsElementPresent(btnContinue))
            {
                IWebElement Button_Continue = new Elements().ById(btnContinue);
                Button_Continue.Click();
            }
            driver.Wait(2);
            // Navagation to Handle Call screen 
            IWebElement Link_Actionable_Dashboard = new Elements().ByXpath("//{0}[contains({1}, '{2}')] ", new string[] { "a", "text()", "Handle Call" });
            Link_Actionable_Dashboard.Click();
            // Verify Page Header
            string pageXpath = "//div[contains(@id, 'tfhPerspectives')][contains(@style,'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//{0}[contains({1}, '{2}')]";
            IWebElement PageHeader = new Elements().ByXpath(pageXpath, new string[] { "h1", "@id", "lblIdentifyPerson" });
            if (!PageHeader.TextEqual("Identify Person")) { throw new Exception("Failed"); }
            // Enter Identify Person search fields
            IWebElement LastNameText = new Elements().ByXpath(pageXpath, new string[] { "input", "@id", "txtLastName" });
            LastNameText.SendKeys("Smith");
            IWebElement FirstNameText = new Elements().ByXpath(pageXpath, new string[] { "input", "@id", "txtFirstName" });
            FirstNameText.SendKeys("Jan");
            IWebElement ZipText = new Elements().ByXpath(pageXpath, new string[] { "input", "@id", "txtZip" });
            ZipText.SendKeys("635524");
            IWebElement DobText = new Elements().ByXpath(pageXpath, new string[] { "input", "@id", "datDob" });
            DobText.SendKeys("07/01/1946");
            IWebElement ViewProfileCheckbox = new Elements().ByXpath(pageXpath, new string[] { "input", "@id", "chkLaunchProfile" });
            ViewProfileCheckbox.Click();
            IWebElement SearchButton = new Elements().ByXpath(pageXpath, new string[] { "button", "@id", "btnSearch" });
            SearchButton.Click();
            // Locate the Results Table
            IWebElement ResultsTable = new Elements().ByXpath(pageXpath, new string[] { "div", "@id", "lstPerson_table" });
            // Locate first entry in DOB column in Results Table 
            IWebElement DobResultsTable = new Elements().ByXpath(pageXpath, new string[] { "a", "@id", "lstPerson_row0_col5" });
            // Verify DOB = expected results
            if (!DobResultsTable.TextEqual("07/01/1946")) { throw new Exception("Failed");}
            // Locate and click Wrapup button
            IWebElement WrapupButton = new Elements().ByXpath("//button[contains(@id, 'btnWrapup') and not(contains(@id, 'info'))]");
            WrapupButton.Click();
            // Locate and select Disposition Select option X-Research
            IWebElement DispositionSelect = new Elements().ByXpath("//div[contains(@id, 'tfhPerspectives')][contains(@style, 'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//select[contains(@id, 'optReasonCodes')]");
            DispositionSelect.SelectOptionByText("X-Research");
            // Locate and select YesNo Select option Yes
            IWebElement YesNoSelect = new Elements().ByXpath("//div[contains(@id, 'tfhPerspectives')][contains(@style, 'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//select[contains(@id, 'optYesNo')]");
            YesNoSelect.SelectOptionByText("Yes");
            driver.Wait(2);
            // Locate and click confirm button
            IWebElement ConfirmButton = new Elements().ByXpath("//div[contains(@id, 'tfhPerspectives')][contains(@style, 'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//button[contains(@id, 'btnConfirm')]");
            ConfirmButton.Click();
            driver.Wait(2);
            // Locate and click logout button
            IWebElement LogoutButton = new Elements().ById("f7_btnLogout");
            LogoutButton.Click();
            IWebElement ConfirmLogoutButton = new Elements().ByXpath("//button[contains(@id, 'btnConfirm')]");
            ConfirmLogoutButton.Click();
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

        public static void SelectOptionByText(this IWebElement element, string optionText)
        {
            new SelectElement(element).SelectByText(optionText);
        }

    }
        
}
