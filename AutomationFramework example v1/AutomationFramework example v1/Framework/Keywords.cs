using AutomationFramework_example_v1.Framework.Log.ExampleLogger;
using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using System;

namespace AutomationFramework_example_v1.Framework
{
    static class Keywords
    {
        private static IWebDriver driver;
        private static ProjectInfo projectInfo;
        private static StepInfo stepInfo;
        private static Suite suite;
        private static TestInfo testInfo;

        public static void Execute(this IWebDriver driver, TestInfo testInfo, ProjectInfo projectInfo, Suite suite, StepInfo stepInfo)
        {
            Keywords.driver = driver;
            Keywords.stepInfo = stepInfo;
            Keywords.testInfo = testInfo;
            Keywords.projectInfo = projectInfo;
            Keywords.suite = suite;
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

        private static void Execute(this StepInfo currentStep)
        {
            Controller.ExecuteStep(testInfo, projectInfo, suite, currentStep);
        }

        private static void Login()
        {
            StepInfo currentStep = null;
            if (stepInfo.parameters.Split(',').Length < 2)
            {
                throw new Exception("Invalid number of parameters supplied for Login keyword - Step: " + stepInfo.id);
            }
            string[] parameters = stepInfo.parameters.Split(',');
            string userName = parameters[0].Trim();
            string password = parameters[1].Trim();

            currentStep = new StepInfo();
            currentStep.controlName = "inputUserName";
            currentStep.action = "SendKeys";
            currentStep.parameters = userName;
            currentStep.Execute();

            currentStep = new StepInfo();
            currentStep.controlName = "inputPassword";
            currentStep.action = "SendKeys";
            currentStep.parameters = password;
            currentStep.Execute();

            driver.Wait(1);

            currentStep = new StepInfo();
            currentStep.controlName = "buttonLogin";
            currentStep.action = "Click";
            currentStep.Execute();

            currentStep = new StepInfo();
            currentStep.controlName = "loginButtonContinue";
            currentStep.action = "Click";
            currentStep.flags = "ifPresent";
            currentStep.Execute();

            driver.Wait(30);
        }

        

        private static void Logout()
        {
            Wrapup();
            // Locate and click logout button
            StepInfo currentStep = new StepInfo();
            currentStep.controlName = "buttonLogout";
            currentStep.action = "Click";
            currentStep.Execute();

            currentStep = new StepInfo();
            currentStep.controlName = "buttonConfirm";
            currentStep.action = "Click";
            currentStep.Execute();
        }

        private static void Wrapup()
        {
            By buttonWrapup = By.XPath("//button[contains(@id, 'btnWrapup') and not(contains(@id, 'info'))]");
            if (driver.IsElementPresent(buttonWrapup))
            {

                StepInfo currentStep = new StepInfo();
                currentStep.controlName = "buttonWrapup";
                currentStep.action = "Click";
                currentStep.flags = "ifPresent";
                currentStep.Execute();

                currentStep = new StepInfo();
                currentStep.controlName = "wrapupSelectDisposition";
                currentStep.action = "SelectOptionByText";
                currentStep.parameters = "X-Research";
                currentStep.Execute();

                // Locate and select YesNo Select option Yes
                currentStep = new StepInfo();
                currentStep.controlName = "wrapupSelectYesNo";
                currentStep.action = "SelectOptionByText";
                currentStep.parameters = "Yes";
                currentStep.Execute();

                driver.Wait(2);

                // Locate and click confirm button
                currentStep = new StepInfo();
                currentStep.controlName = "wrapupConfirmButton";
                currentStep.action = "Click";
                currentStep.Execute();
            }
            driver.Wait(2);
        }
    }
}
