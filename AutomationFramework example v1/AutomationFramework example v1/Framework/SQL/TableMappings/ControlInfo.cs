using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class ControlInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("id")]
        public int id = 0;

        [ColumnMap("controlName")]
        public string controlName;

        [ColumnMap("pathType")]
        public string pathType;

        [ColumnMap("xpathNodeType")]
        public string xpathNodeType;

        [ColumnMap("xpathParameterName")]
        public string xpathParameterName;

        [ColumnMap("xpathParameterValue")]
        public string xpathParameterValue;

        [ColumnMap("xpathName")]
        public string xpathName;

        [ColumnMap("controlID")]
        public string controlId;

        [ColumnMap("controlCss")]
        public string controlCss;

        [ColumnMap("projectName")]
        public string projectName;

        public ControlInfo Populate(string controlName, string projectName)
        {
            Command cmd = new Command("getControlInformation");
            cmd.AddParameter("controlName", controlName);
            cmd.AddParameter("projectName", projectName);
            ControlInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            PopulateLogData();
            return result;

        }

        private void PopulateLogData()
        {
            TestLogData.pathType = pathType;
            TestLogData.xpathNodeType = xpathNodeType;
            TestLogData.xpathParameterName = xpathParameterName;
            TestLogData.xpathParameterValue = xpathParameterValue;
            TestLogData.xpathName = xpathName;
            TestLogData.controlId = controlId;
            TestLogData.controlCss = controlCss;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
