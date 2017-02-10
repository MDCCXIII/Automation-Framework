using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                default:
                    throw new Exception("The Keyword " + keyword + " is not a valid Keyword.");
            }
        }

        private static void Login()
        {
            if(stepInfo.parameters.Split(',').Length < 2)
            {
                throw new Exception("Invalid number of parameters supplied for Login keyword - Step: " + stepInfo.stepNumber);
            }
            string[] parameters = stepInfo.parameters.Split(',');
            string userName = parameters[0].Trim();
            string password = parameters[1].Trim();

            IWebElement Text_UserName = Elements.ById("userName");
            Text_UserName.SendKeys(userName);
            IWebElement Text_Password = Elements.ById("password");
            Text_Password.SendKeys(password);
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
    }
}
