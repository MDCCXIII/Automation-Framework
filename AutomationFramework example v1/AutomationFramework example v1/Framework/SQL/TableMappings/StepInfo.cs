using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;
using System.Collections.Generic;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class StepInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("id")]
        public int id = 0;

        [ColumnMap("controlName")]
        public string controlName = "";

        [ColumnMap("action")]
        public string action = "";

        [ColumnMap("keyword")]
        public string keyword = "";

        [ColumnMap("parameters")]
        public string parameters = "";

        [ColumnMap("failCondition")]
        public string failCondition = "";



        public List<StepInfo> Populate(string procedureName)
        {
            Command cmd = new Command(procedureName);
            List<StepInfo> result = this.ExecuteStoredProcedure(cmd);
            cmd.Dispose();
            return result;
        }

#pragma warning restore 0169
#pragma warning restore 0649
    }
    static class stepInfoExtensions
    {
        public static void PopulateLogData(this StepInfo stepInfo)
        {
            TestLogData.DefaultStepValues();
            TestLogData.stepNumber++;
            TestLogData.controlName = stepInfo.controlName;
            TestLogData.actionName = stepInfo.action;
            TestLogData.keywordName = stepInfo.keyword;
            TestLogData.stepParameters = stepInfo.parameters;
            TestLogData.failCondition = stepInfo.failCondition;
        }
    }
}
