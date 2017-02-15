
namespace AutomationFramework_example_v1.Framework.Log.LogObjects
{
    class TestLogData : ILog
    {
        public static string testName = "";
        public static string testedBrowser = "";
        public static string projectName = "";
        public static string testDescription = "";
        public static string projectUrl = "";
        public static string controlName = "";
        public static string actionName = "";
        public static string keywordName = "";
        public static string requiredActionParameters = "";
        public static string optionalActionParameters = "";
        public static string actionDescription = "";
        public static string requiredKeywordParameters = "";
        public static string optionalKeywordParameters = "";
        public static string keywordDescription = "";
        public static string pathType = "";
        public static string xpathNodeType = "";
        public static string xpathParameterName = "";
        public static string xpathParameterValue = "";
        public static string xpathName = "";
        public static string xpath = "";
        public static string controlId = "";
        public static string controlCss = "";
        public static string identifiedControlName = "";
        public static string stepParameters = "";
        public static string failCondition = "";
        public static string stepProcedureName = "";
        public static int stepNumber = 0;

        public static void DefaultTestValues()
        {
            testName = "";
            //testedBrowser = "";
            projectName = "";
            testDescription = "";
            //projectUrl = "";
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
            stepProcedureName = "";
            stepNumber = 0;
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
        }
    }
}
