using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;

namespace AutomationFramework_example_v1.Framework
{
    static class Keywords
    {
        private static IWebDriver driver;
        private static StepInfo stepInfo;

        public static void Execute(this IWebDriver driver, StepInfo stepInfo)
        {
            Keywords.driver = driver;
            Keywords.stepInfo = stepInfo;

            LaunchKeyword(stepInfo.keyword);
        }

        private static void LaunchKeyword(string keyword)
        {
            switch (keyword.ToLower())
            {
                case "login":
                    Login();
                    break;
                case "logout":
                    Logout();
                    break;
                default:
                    throw new Exception("The Keyword " + keyword + " is not a valid Keyword.");
            }
        }

        private static void Login()
        {
            if(stepInfo.parameters.Split(',').Length < 2)
            {
                throw new Exception("Invalid number of parameters supplied for Login keyword - Step: " + stepInfo.id);
            }
            string[] parameters = stepInfo.parameters.Split(',');
            string userName = parameters[0].Trim();
            string password = parameters[1].Trim();

            IWebElement Text_UserName = Elements.ById("userName");
            Text_UserName.SendKeys(userName);
            IWebElement Text_Password = Elements.ById("password");
            Text_Password.SendKeys(password);
            driver.Wait(1);
            IWebElement Button_Login = Elements.ById("loginButton");
            Button_Login.Click();
            By btnContinue = By.Id("f5_btnContinue");
            // Work around if another user is login
            if (driver.IsElementPresent(btnContinue))
            {
                IWebElement Button_Continue = Elements.ById(btnContinue);
                Button_Continue.Click();
            }
            driver.Wait(10);
        }

        private static void Logout()
        {
            Wrapup();
            // Locate and click logout button
            IWebElement LogoutButton = Elements.ById("f7_btnLogout");
            LogoutButton.Click();
            IWebElement ConfirmLogoutButton = Elements.ByXpath("//button[contains(@id, 'btnConfirm')]");
            ConfirmLogoutButton.Click();
        }

        private static void Wrapup()
        {
            By buttonWrapup = By.XPath("//button[contains(@id, 'btnWrapup') and not(contains(@id, 'info'))]");
            if (driver.IsElementPresent(buttonWrapup))
            {
                IWebElement WrapupButton = Elements.ByXpath(buttonWrapup);
                WrapupButton.Click();
                // Locate and select Disposition Select option X-Research
                IWebElement DispositionSelect = Elements.ByXpath("//div[contains(@id, 'tfhPerspectives')][contains(@style, 'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//select[contains(@id, 'optReasonCodes')]");
                DispositionSelect.SelectOptionByText("X-Research");
                // Locate and select YesNo Select option Yes
                IWebElement YesNoSelect = Elements.ByXpath("//div[contains(@id, 'tfhPerspectives')][contains(@style, 'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//select[contains(@id, 'optYesNo')]");
                YesNoSelect.SelectOptionByText("Yes");
                driver.Wait(2);
                // Locate and click confirm button
                IWebElement ConfirmButton = Elements.ByXpath("//div[contains(@id, 'tfhPerspectives')][contains(@style, 'visibility: visible')]//div[contains(@id, 'fhdVerbRunner')]/div[contains(@id, 'innerForm')][not(contains(@style, 'display: none'))]//button[contains(@id, 'btnConfirm')]");
                ConfirmButton.Click();
                driver.Wait(2);
            }
        }
    }
}
