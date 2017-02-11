using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework
{
    static class Actions
    {
        private static IWebElement control;
        private static StepInfo step;

        public static void Execute(this IWebElement control, StepInfo stepInfo)
        {
            Actions.control = control;
            Actions.step = stepInfo;
            LoadAction(step.action);
        }

        private static void LoadAction(string actionName)
        {
            switch (actionName.ToLower())
            {
                case "click":
                    Click();
                    break;
                case "inputtext":
                    SendKeys();
                    break;
                case "verifytextequals":
                    VerifyTextEqual(step.parameters);
                    break;
                default:
                    throw new Exception("The Action " + actionName + " is not a valid action name.");
            }
        }

        private static void SendKeys()
        {
            if (step.parameters.ToLower().Contains("clearbeforesending"))
            {
                control.Click();
                control.Clear();
                step.parameters = step.parameters.Replace("ClearBeforeSending", "");
                step.parameters = step.parameters.Trim().Trim(',').Trim();
            }
            control.SendKeys(step.parameters);
        }

        private static void Click()
        {
            int attempt = 0;
            while (attempt < 4)
            {
                try
                {
                    attempt++;
                    control.Click();
                    break;

                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                    if(attempt > 2)
                    {
                        throw ex;
                    }
                }
            }
        }

        private static void VerifyTextEqual(string parameters)
        {
            if(parameters == "")
            {
                throw new Exception("No parameters provided for step number " + step.stepNumber);
            }

            if(!control.TextEqual(parameters))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did not equal \"" + parameters +"\".");
            }
        }
    }
}
