using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class TestInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("id")]
        public int id;

        [ColumnMap("testName")]
        public string testName;

        [ColumnMap("stepProcedureName")]
        public string stepProcedureName;

        [ColumnMap("testDescription")]
        public string testDescription;

        [ColumnMap("projectName")]
        public string projectName;

        public TestInfo Populate(string testName, string projectName)
        {
            Command cmd = new Command("getTestInformation");
            cmd.AddParameter("testName", testName);
            cmd.AddParameter("projectName", projectName);
            TestInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            PopulateLogData(result);
            return result;

        }

        private void PopulateLogData(TestInfo testInfo)
        {
            TestLogData.testDescription = testInfo.testDescription;
            TestLogData.stepProcedureName = testInfo.stepProcedureName;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
