using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;
using System.Collections.Generic;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class Suite : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("Id")]
        public int Id;

        [ColumnMap("testName")]
        public string testName;

        [ColumnMap("Execute")]
        public bool execute;

        [ColumnMap("testedBrowser")]
        public string browser;

        [ColumnMap("projectName")]
        public string projectName;

        public List<Suite> Populate()
        {
            TestLogData.DefaultTestValues();
            Command cmd = new Command("getSuiteInformation");
            List<Suite> result = this.ExecuteStoredProcedure(cmd);
            cmd.Dispose();
            return result;

        }


#pragma warning restore 0169
#pragma warning restore 0649
    }
    static class suiteExtensions
    {
        internal static void PopulateLogData(this Suite suite)
        {
            TestLogData.DefaultTestValues();
            TestLogData.testName = suite.testName;
            TestLogData.testedBrowser = suite.browser;
            TestLogData.projectName = suite.projectName;
        }
    }
}
