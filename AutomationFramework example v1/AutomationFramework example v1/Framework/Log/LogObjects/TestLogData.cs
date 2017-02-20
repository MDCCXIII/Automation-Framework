
using System;

namespace AutomationFramework_example_v1.Framework.Log.LogObjects
{
    class TestLogData : ILog
    {
        public static string executionDate = DateTime.Now.Date.ToString("MM/dd/yyyy");
        public static string executionStartTime = DateTime.Now.TimeOfDay.ToString();
        public static string suiteExecutionTime = "";

        public static string testName = "";
        public static string testExecutionTime = "";
        public static string testResult = "Pass";
        public static string testDescription = "";
        public static string stepProcedureName = "";
        public static string projectName = "";
        public static string testedBrowser = "";
        public static string projectUrl = "";

        public static int stepNumber = 0;
        public static string stepExecutionTime = "";
        public static string stepResult = "Pass";
        public static string warning = "";

        public static string keywordName = "";
        public static string keywordDescription = "";
        public static string requiredKeywordParameters = "";
        public static string optionalKeywordParameters = "";

        public static string actionName = "";
        public static string actionDescription = "";
        public static string requiredActionParameters = "";
        public static string optionalActionParameters = "";

        public static string stepParameters = "";

        public static string controlName = "";
        public static string pathType = "";
        public static string xpathName = "";
        public static string xpath = "";
        public static string xpathNodeType = "";
        public static string xpathParameterName = "";
        public static string xpathParameterValue = "";
        public static string controlId = "";
        public static string controlCss = "";

        public static string controlIdentificationTime = "";
        public static string identifiedControlName = "";
        public static string identifiedControlId = "";
        public static string identifiedControlTagName = "";
        public static string identifiedControlText = "";
        public static bool identifiedControlIsDisplayed = false;
        public static bool identifiedControlIsEnabled = false;
        public static bool identifiedControlIsSelected = false;
        
        public static string failCondition = "";
        public static string exceptionMessage = "";
        public static Exception ex = null;
        
        public static void DefaultTestValues()
        {
            //testedBrowser = "";
            //projectUrl = "";
            testExecutionTime = "";
            testName = "";
            testResult = "Pass";
            projectName = "";
            testDescription = "";
            stepProcedureName = "";
            stepNumber = 0;
            DefaultStepValues();
        }

        public static void DefaultStepValues()
        {
            controlName = "";
            actionName = "";
            keywordName = "";
            requiredActionParameters = "";
            optionalActionParameters = "";
            actionDescription = "";
            requiredKeywordParameters = "";
            optionalKeywordParameters = "";
            keywordDescription = "";
            pathType = "";
            xpathNodeType = "";
            xpathParameterName = "";
            xpathParameterValue = "";
            xpathName = "";
            xpath = "";
            controlId = "";
            controlCss = "";
            identifiedControlName = "";
            stepParameters = "";
            failCondition = "";
            identifiedControlId = "";
            identifiedControlTagName = "";
            identifiedControlText = "";
            identifiedControlIsDisplayed = false;
            identifiedControlIsEnabled = false;
            identifiedControlIsSelected = false;
            stepResult = "Pass";
            exceptionMessage = "";
            stepExecutionTime = "";
            controlIdentificationTime = "";
            ex = null;
            warning = "";
        }
    }
}
