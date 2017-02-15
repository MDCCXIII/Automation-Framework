﻿using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;
using System.Collections.Generic;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class StepInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("id")]
        public int id;

        [ColumnMap("controlName")]
        public string controlName;

        [ColumnMap("action")]
        public string action;

        [ColumnMap("keyword")]
        public string keyword;

        [ColumnMap("parameters")]
        public string parameters;

        [ColumnMap("failCondition")]
        public string failCondition;



        public List<StepInfo> Populate(string procedureName)
        {
            Command cmd = new Command(procedureName);
            List<StepInfo> result = this.ExecuteStoredProcedure(cmd);
            cmd.Dispose();
            return result;
        }

        public void PopulateLogData()
        {
            TestLogData.DefaultStepValues();
            TestLogData.stepNumber++;
            TestLogData.controlName = controlName;
            TestLogData.actionName = action;
            TestLogData.keywordName = keyword;
            TestLogData.stepParameters = parameters;
            TestLogData.failCondition = failCondition;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}