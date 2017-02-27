﻿using AutomationFramework_example_v1.Framework.Log.LogObjects;
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

        public static void Execute(this IWebElement control, StepInfo stepInfo, ControlInfo controlInfo, XpathInfo xpathInfo)
        {
            Actions.control = control;
            Actions.controlInfo = controlInfo;
            Actions.xpathInfo = xpathInfo;
            step = stepInfo;
            step.parameters = step.parameters.Trim('\r').Trim('\n');
            
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
        public const string VERIFYTEXTNOTEQUALS = "verifytextnotequals";
        public const string VERIFYTEXTNOTCONTAINS = "verifytextnotcontains";
        private static ControlInfo controlInfo;
        private static XpathInfo xpathInfo;

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
                case VERIFYTEXTNOTCONTAINS:
                    VerifyTextNotContains(step.parameters);
                    break;
                case VERIFYTEXTNOTEQUALS:
                    VerifyTextNotEqual(step.parameters);
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
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (!controlText.Contains(parameters) && !controlValue.Contains(parameters))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did not contain \"" + parameters + "\". \n Expected: \"" + parameters + "\"\n Actual: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
            }
        }

        private static void VerifyTextNotContains(string parameters)
        {
            if (parameters == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (controlText.Contains(parameters) && controlValue.Contains(parameters))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did contain \"" + parameters + "\". \n Expected to not contain: \"" + parameters + "\"\n Actual: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
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
            if (step.flags.ToLower().Contains("clearbeforesending"))
            {
                Click();
                control.Clear();
            }
            control.SendKeys(step.parameters);
        }

        private static void InputDate()
        {
            if (step.flags.ToLower().Contains("clearbeforesending"))
            {
                Click();
                control.Clear();
            }
            //pulled the return key as it was causing the page to validate
            control.SendKeys(step.parameters);//+ Keys.Return
        }

        private static void Click()
        {
            int attempt = 0;
            while (attempt < 2)
            {
                try
                {
                    attempt++;
                    Thread.Sleep(1000);
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
            if (parameters == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (!controlText.Equals(parameters) && !controlValue.Equals(parameters))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did not equal \"" + parameters + "\". \n Expected: \"" + parameters + "\"\n Actual Text: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
            }
        }

        private static void VerifyTextNotEqual(string parameters)
        {
            if (parameters == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (controlText.Equals(parameters) && controlValue.Equals(parameters))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text is equal to \"" + parameters + "\". \n Expected to not equal: \"" + parameters + "\"\n Actual Text: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
            }
        }

        private static void WaitForValue()
        {
            int i = 0;
            while (control.Update().Text.Equals("") && control.Update().GetAttribute("value").Equals(""))
            {
                Thread.Sleep(3000);
                if (i++ >= 4)
                {
                    TestLogData.warning = "Unable to capture the " + step.controlName + "'s text or value. Attribute is empty (\"\")";
                    break;
                }
            }
        }

        private static IWebElement Update(this IWebElement control)
        {
                control = Elements.ById(Elements.GetIdentifier(controlInfo, xpathInfo));
            return control;
        }
       
    }
    
}
