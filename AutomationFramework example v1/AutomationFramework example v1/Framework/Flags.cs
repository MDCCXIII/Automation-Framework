using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.TableMappings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework
{
    class Flags
    {
        public static bool CheckIfPresent(StepInfo step, ControlInfo controlInfo, XpathInfo pathInfo)
        {
            if (step.flags != null && step.flags.Contains("ifPresent"))
            {
                try
                {
                    Driver.wait.IsElementPresent(Elements.GetIdentifier(controlInfo, pathInfo));
                    return true;
                }
                catch
                {
                    TestLogData.warning = "The control was not present.";
                    return false;
                }
            }

            return true;
        }

        public static bool PassIfControlNotPresent(StepInfo step, ControlInfo controlInfo, XpathInfo pathInfo)
        {
            if (step.flags != null && step.flags.Contains("notPresent"))
            {
                try
                {
                    Driver.wait.IsElementPresent(Elements.GetIdentifier(controlInfo, pathInfo));
                    throw new Exception("The Control was present.");
                }
                catch
                {
                    TestLogData.stepResult = "Pass";
                    TestLogData.identifiedControlId = "The Control was not present on the screen.";
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Provides an access point for debug specific points (Don't forget to set the breakpoint)
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public static bool CheckIfDebug(StepInfo step)
        {
            if (step.flags != null && step.flags.Contains("debug"))
            {
                Debug.WriteLine("debug step: " + step.id);
            }
            return true;
        }
    }
}
