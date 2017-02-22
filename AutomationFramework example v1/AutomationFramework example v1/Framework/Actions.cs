using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace AutomationFramework_example_v1.Framework
{
    static class Actions
    {
        private static IWebElement control;
        private static StepInfo step;

        public static void Execute(this IWebElement control, StepInfo stepInfo)
        {
            Actions.control = control;
            step = stepInfo;
            
            LoadAction(step.action);
        }
        public const string CLICK = "click";
        public const string SETSTATECHECKED = "setstatechecked";
        public const string SETSTATEUNCHECKED = "setstateunchecked";
        public const string INPUTTEXT = "inputtext";
        public const string INPUTDATE = "inputdate";
        public const string VERIFYTEXTEQUALS = "verifytextequals";
        public const string SELECTOPTIONBYTEXT = "selectoptionbytext";
        public const string VERIFYTEXTCONTAINS = "verifytextcontains";

        private static void LoadAction(string actionName)
        {
            
            switch (actionName.ToLower())
            {
                case CLICK:
                    Click();
                    break;
                case SETSTATECHECKED:
                    SetCheckedState(true);
                    break;
                case SETSTATEUNCHECKED:
                    SetCheckedState(false);
                    break;
                case INPUTTEXT:
                    SendKeys();
                    break;
                case INPUTDATE:
                    InputDate();
                    break;
                case VERIFYTEXTEQUALS:
                    VerifyTextEqual(step.parameters);
                    break;
                case SELECTOPTIONBYTEXT:
                    SelectOptionByText(step.parameters);
                    break;
                case VERIFYTEXTCONTAINS:
                    VerifyTextContains(step.parameters);
                    break;
                default:
                    throw new Exception("The Action " + actionName + " is not a valid action name.");
            }
        }

        private static void VerifyTextContains(string parameters)
        {
            if (parameters == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            if (!control.Text.Contains(parameters) && !control.GetAttribute("value").Contains(parameters))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did not contain \"" + parameters + "\". \n Expected: \"" + parameters + "\"\n Actual: \"" + control.Text + "\" Actual Value: \"" + control.GetAttribute("value") + "\"");
            }
        }

        private static void SelectOptionByText(string parameters)
        {
            control.SelectOptionByText(parameters);
        }

        private static void SetCheckedState(bool v)
        {
            if (!control.GetAttribute("checked").Equals(v.ToString()))
            { 
                Click();
            }
        }

        private static void SendKeys()
        {
            if (step.parameters.ToLower().Contains("clearbeforesending"))
            {
                Click();
                control.Clear();
                step.parameters = step.parameters.Replace("ClearBeforeSending", "");
                step.parameters = step.parameters.Trim().Trim(',').Trim();
            }
            control.SendKeys(step.parameters);
        }

        private static void InputDate()
        {
            if (step.parameters.ToLower().Contains("clearbeforesending"))
            {
                Click();
                control.Clear();
                step.parameters = step.parameters.Replace("ClearBeforeSending", "");
                step.parameters = step.parameters.Trim().Trim(',').Trim();
            }
            //pulled the return key as it was causing the page to validate
            control.SendKeys(step.parameters );//+ Keys.Return
        }

        private static void Click()
        {
            int attempt = 0;
            while (attempt < 2)
            {
                try
                {
                    attempt++;
                    Driver.wait.Until(ExpectedConditions.ElementToBeClickable(control)).Click();
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
                throw new Exception("No parameters provided for step number " + step.id);
            }

            if(!control.Text.Equals(parameters) && !control.Update().GetAttribute("value").Equals(parameters))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did not equal \"" + parameters + "\". \n Expected: \"" + parameters + "\"\n Actual Text: \"" + control.Text + "\" Actual Value: \"" + control.GetAttribute("value") + "\"");
            }
        }

        private static IWebElement Update(this IWebElement control)
        {
            return Elements.ById(By.Id(control.GetCssValue("id")));
        }
       
    }
    
}
