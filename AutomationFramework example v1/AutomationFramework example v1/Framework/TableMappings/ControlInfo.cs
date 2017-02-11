using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class ControlInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("controlId")]
        public int controlId = 0;

        [ColumnMap("controlName")]
        public string controlName;

        [ColumnMap("identifyingNodeType")]
        public string identifyingNodeType;

        [ColumnMap("identifyingParameterName")]
        public string identifyingParameterName;

        [ColumnMap("identifyingParameterValue")]
        public string identifyingParameterValue;

        [ColumnMap("pathName")]
        public string pathName;

        [ColumnMap("projectName")]
        public string projectName;

        public ControlInfo Populate(string controlName, string projectName)
        {
            Command cmd = new Command("getControlInformation");
            cmd.AddParameter("controlName", controlName);
            cmd.AddParameter("projectName", projectName);
            ControlInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            return result;

        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
