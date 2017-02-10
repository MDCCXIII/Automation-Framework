using AutomationFramework_example_v1.Framework.SQL;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1
{
    class StepInfo : TableMap
    {
        [ColumnMap("stepNumber")]
        public int stepNumber;

        [ColumnMap("controlName")]
        public string controlName;

        [ColumnMap("expectedValue")]
        public string expectedValue;

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
    }
}
