using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class KeywordInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("Id")]
        public int Id;

        [ColumnMap("keywordName")]
        public string name;

        [ColumnMap("requiredParameters")]
        public string requiredParameters;

        [ColumnMap("optionalParameters")]
        public string optionalParameters;

        [ColumnMap("applicableProject")]
        public string applicableProject;

        [ColumnMap("keywordDescription")]
        public string description;

        public KeywordInfo Populate(string keywordName, string projectName)
        {
            Command cmd = new Command("getKeywordInformation");
            cmd.AddParameter("keywordName", keywordName);
            cmd.AddParameter("projectName", projectName);
            KeywordInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            PopulateLogData();
            return result;

        }

        private void PopulateLogData()
        {
            TestLogData.requiredKeywordParameters = requiredParameters;
            TestLogData.optionalKeywordParameters = optionalParameters;
            TestLogData.keywordDescription = description;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
