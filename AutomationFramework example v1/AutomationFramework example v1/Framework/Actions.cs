using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL.DONT_TOUCH;
using AutomationFramework_example_v1.Framework.TableMappings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace AutomationFramework_example_v1.Framework
{
    public class Actions : ActionMap
    {
        public static IWebElement control;
        public static StepInfo step;
        public static ControlInfo controlInfo;
        public static XpathInfo xpathInfo;
        
        public static void LoadAction()
        {
            MethodInfo method = typeof(Actions).GetMethod(step.action);
            if (method == null) throw new Exception("Improper step action name: \"" + step.action + "\".");
            try
            {
                Type deligateType = Expression.GetDelegateType(
                        (from parameter in method.GetParameters() select parameter.ParameterType)
                        .Concat(new[] { method.ReturnType }).ToArray());
                if (step.parameters.Equals(""))
                {
                    method.CreateDelegate(deligateType).DynamicInvoke();
                }
                else
                {
                    method.CreateDelegate(deligateType).DynamicInvoke(step.parameters);
                }
                
            }
            catch (ArgumentException)
            {
            }
        }



        #region Actions

        #region Clicks
        [ActionMap("Clicks on a Control.")]
        public static void Click()
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

        [ActionMap("Sets the state of a checkbox control to checked if not already in that state.")]
        public static void SetCheckedStateChecked()
        {
            if (!control.GetAttribute("checked").Equals(true))
            { 
                Click();
            }
        }

        [ActionMap("Sets the state of a checkbox control to not checked if not already in that state.")]
        public static void SetCheckedStateUnchecked()
        {
            if (!control.GetAttribute("checked").Equals(false))
            {
                Click();
            }
        }
        #endregion

        #region Inputs

        [ActionMap("Sends key information to a control to emulate typing the provided text.")]
        public static void SendKeys(string inputText)
        {
            if (step.flags.ToLower().Contains("clearbeforesending"))
            {
                Click();
                control.Clear();
            }
            control.SendKeys(inputText);
        }

        [ActionMap("Inputs a date into a Datepicker.")]
        public static void InputDate(string inputDate)
        {
            if (step.flags.ToLower().Contains("clearbeforesending"))
            {
                Click();
                control.Clear();
            }
            //pulled the return key as it was causing the page to validate
            control.SendKeys(inputDate);//+ Keys.Return
        }

        [ActionMap("Selects an option in a selection dropdown by the options text.")]
        public static void SelectOptionByText(string optionText)
        {
            new SelectElement(control).SelectByText(optionText);
        }

        #endregion

        #endregion

        #region Verification

        [ActionMap("Checks if a controls text or value matches the expected text.")]
        public static void VerifyTextEqual(string value)
        {
            if (value == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (!controlText.Equals(value) && !controlValue.Equals(value))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did not equal \"" + value + "\". \n Expected: \"" + value + "\"\n Actual Text: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
            }
        }

        [ActionMap("Checks if a controls text or value does not equal the expected text.")]
        public static void VerifyTextNotEqual(string value)
        {
            if (value == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (controlText.Equals(value) && controlValue.Equals(value))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text is equal to \"" + value + "\". \n Expected to not equal: \"" + value + "\"\n Actual Text: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
            }
        }

        [ActionMap("Checks if a controls text or value contains the expected text.")]
        public static void VerifyTextContains(string value)
        {
            if (value == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (!controlText.Contains(value) && !controlValue.Contains(value))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did not contain \"" + value + "\". \n Expected: \"" + value + "\"\n Actual: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
            }
        }

        [ActionMap("Checks if a controls text or value does not contain the expected text.")]
        public static void VerifyTextNotContains(string value)
        {
            if (value == "")
            {
                throw new Exception("No parameters provided for step number " + step.id);
            }
            WaitForValue();
            string controlText = control.Update().Text;
            string controlValue = control.Update().GetAttribute("value");
            if (controlText == null) controlText = "";
            if (controlValue == null) controlValue = "";
            if (controlText.Contains(value) && controlValue.Contains(value))
            {
                throw new Exception("Validation Error: the control " + step.controlName + "'s text did contain \"" + value + "\". \n Expected to not contain: \"" + value + "\"\n Actual: \"" + controlText + "\" Actual Value: \"" + controlValue + "\"");
            }
        }
        #endregion

        #region ExtractionLayer
        public static void WaitForValue()
        {
            int i = 0;
            while (Actions.control.Update().Text.Equals("") && Actions.control.Update().GetAttribute("value").Equals(""))
            {
                Thread.Sleep(3000);
                if (i++ >= 4)
                {
                    TestLogData.warning = "Unable to capture the " + Actions.step.controlName + "'s text or value. Attribute is empty (\"\")";
                    break;
                }
            }
        }
        #endregion
    }

    public static class ActionExtensions
    {
        public static IWebElement Update(this IWebElement control)
        {
            return Actions.control = Elements.ById(Elements.GetIdentifier(Actions.controlInfo, Actions.xpathInfo));
        }
        public static void Execute(this IWebElement control, StepInfo stepInfo, ControlInfo controlInfo, XpathInfo xpathInfo)
        {
            Actions.control = control;
            Actions.controlInfo = controlInfo;
            Actions.xpathInfo = xpathInfo;
            Actions.step = stepInfo;
            Actions.step.parameters = Actions.step.parameters.Trim('\r').Trim('\n');
            
            Actions.LoadAction();
        }
        
    }
}
