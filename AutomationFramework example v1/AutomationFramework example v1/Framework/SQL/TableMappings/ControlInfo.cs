using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    public class ControlInfo : TableMap
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
            ControlInfo result = this.ExecuteQuery(cmd)[0];
            cmd.Dispose();
            PopulateLogData(result);
            return result;

        }

        private void PopulateLogData(ControlInfo controlInfo)
        {
            TestLogData.pathType = controlInfo.pathType;
            TestLogData.xpathNodeType = controlInfo.xpathNodeType;
            TestLogData.xpathParameterName = controlInfo.xpathParameterName;
            TestLogData.xpathParameterValue = controlInfo.xpathParameterValue;
            TestLogData.xpathName = controlInfo.xpathName;
            TestLogData.controlId = controlInfo.controlId;
            TestLogData.controlCss = controlInfo.controlCss;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
