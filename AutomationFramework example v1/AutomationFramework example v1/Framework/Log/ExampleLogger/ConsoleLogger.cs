using AutomationFramework_example_v1.Framework.Log.LogObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework.Log.ExampleLogger
{
    static class ConsoleLogger
    {
        static StringBuilder sb;

        public static void Log()
        {
            Log(sb.ToString());
        }

        public static void LogTestInfo()
        {
            sb = new StringBuilder();
            sb.AppendLine("Test Name: " + TestLogData.testName + " Tested Browser: " + TestLogData.testedBrowser);
            sb.AppendLine("Project Name: " + TestLogData.projectName + " Project Url: " + TestLogData.projectUrl);
            sb.AppendLine("Execution Date: " + TestLogData.executionDate + " Execution Start Time: " + TestLogData.executionStartTime);
            sb.AppendLine("Test step procedure: " + TestLogData.stepProcedureName);
            sb.AppendLine("Test Description: " + TestLogData.testDescription);
            sb.AppendLine("%#Result#%");
            sb.AppendLine("%#ExecutionTime#%");
            sb.AppendLine();
        }

        internal static void Log(Exception ex)
        {
            sb.AppendLine(ex.ToString());
        }

        public static void LogStepInfo()
        {
            sb.AppendLine("Step Number: " + TestLogData.stepNumber);
            if (!TestLogData.warning.Equals(""))
            {
                sb.AppendLine("WARNING: " + TestLogData.warning);
            }
            BuildKeywordInfo();
            BuildActionInfo();
            BuildStepParameterInfo();
            BuildControlIdentificationInfo();
            BuildFailConditionInfo();
        }

        private static void BuildFailConditionInfo()
        {
            if (!TestLogData.failCondition.Equals(""))
            {
                sb.AppendLine("Fail Condition: " + TestLogData.failCondition);
            }
        }

        private static void BuildControlIdentificationInfo()
        {
            if (!TestLogData.controlName.Equals(""))
            {
                sb.AppendLine("Control Name: " + TestLogData.controlName);
                switch (TestLogData.pathType.ToLower())
                {
                    case "xpath":
                        if (!TestLogData.xpathNodeType.Equals("") && !TestLogData.xpathParameterName.Equals("") && !TestLogData.xpathParameterValue.Equals(""))
                        {
                            sb.AppendLine("Identifing Xpath: " + string.Format(TestLogData.xpath, new string[] { TestLogData.xpathNodeType, TestLogData.xpathParameterName, TestLogData.xpathParameterValue }));
                        }
                        else
                        {
                            sb.AppendLine("Identifing Xpath: " + TestLogData.xpath);
                        }
                        break;
                    case "id":
                        sb.AppendLine("Identifing ID: " + TestLogData.controlId);
                        break;
                    case "css":
                        sb.AppendLine("Identifing CSS: " + TestLogData.controlCss);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void BuildStepParameterInfo()
        {
            if (!TestLogData.stepParameters.Equals(""))
            {
                sb.AppendLine("Step Parameters: " + TestLogData.stepParameters.Replace("\r\n",""));
            }
        }

        private static void BuildActionInfo()
        {
            if (!TestLogData.actionName.Equals(""))
            {
                sb.Append("Action Name: " + TestLogData.actionName);
                if (!TestLogData.requiredActionParameters.Equals(""))
                {
                    sb.Append(" Required Parameters: " + TestLogData.requiredActionParameters);
                }
                if (!TestLogData.optionalActionParameters.Equals(""))
                {
                    sb.Append(" Optional Parameters: " + TestLogData.optionalActionParameters);
                }
                sb.AppendLine();
                sb.AppendLine("Action Description: " + TestLogData.actionDescription);
            }
        }

        private static void BuildKeywordInfo()
        {
            if (!TestLogData.keywordName.Equals(""))
            {
                sb.Append("Keyword Name: " + TestLogData.keywordName);
                if (!TestLogData.requiredKeywordParameters.Equals(""))
                {
                    sb.Append(" Required Parameters: " + TestLogData.requiredKeywordParameters);
                }
                if (!TestLogData.optionalKeywordParameters.Equals(""))
                {
                    sb.Append(" Optional Parameters: " + TestLogData.optionalKeywordParameters);
                }
                sb.AppendLine();
                sb.AppendLine("Keyword Description: " + TestLogData.keywordDescription);
            }
        }

        public static void LogStepResult()
        {
            sb.AppendLine("Step " + TestLogData.stepNumber + " - " + TestLogData.stepResult);
            if (!TestLogData.exceptionMessage.Equals(""))
            {
                sb.AppendLine("Error: " + TestLogData.exceptionMessage);
            }
            if (!TestLogData.controlName.Equals(""))
            {
                sb.AppendLine("Indentified Contol Information: ");
                if(TestLogData.identifiedControlName != null && !TestLogData.identifiedControlName.Equals(""))
                {
                    sb.Append("Name: " + TestLogData.identifiedControlName + " ");
                }
                if(TestLogData.identifiedControlId != null && !TestLogData.identifiedControlId.Equals(""))
                {
                    sb.Append("Id: " + TestLogData.identifiedControlId + " ");
                }
                if(TestLogData.identifiedControlTagName != null && !TestLogData.identifiedControlTagName.Equals(""))
                {
                    sb.AppendLine("Tag: " + TestLogData.identifiedControlTagName);
                }

                sb.AppendLine("Displayed = " + TestLogData.identifiedControlIsDisplayed.ToString() + ", Enabled = " + TestLogData.identifiedControlIsEnabled.ToString() + ", Selected = " + TestLogData.identifiedControlIsSelected.ToString());
                if(TestLogData.identifiedControlText != null && !TestLogData.identifiedControlText.Equals(""))
                {
                    sb.AppendLine("Control Text: ");
                    sb.AppendLine(TestLogData.identifiedControlText);
                }
            }
            sb.AppendLine("Total Step Exection Time: " + TestLogData.stepExecutionTime);
            sb.AppendLine();
        }

        public static void LogTestResults()
        {
            sb.Replace("%#Result#%", "Result: " + TestLogData.testResult);
            sb.Replace("%#ExecutionTime#%", "Test Completed: " + DateTime.Now.TimeOfDay.ToString() + " - " + DateTime.Now.Date.ToString("MM/dd/yyyy") + " Total Test Execution Time: " + TestLogData.testExecutionTime);
        }

        public static void Log(string logMessage)
        {
            Debug.WriteLine(logMessage);
        }
    }
}
