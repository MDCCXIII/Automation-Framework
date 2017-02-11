using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class ActionInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("Id")]
        public int Id;

        [ColumnMap("actionName")]
        public string actionName;

        [ColumnMap("requiredParameters")]
        public string requiredParameters;

        [ColumnMap("optionalParameters")]
        public string optionalParameters;

        [ColumnMap("actionDescription")]
        public string actionDescription;

        public ActionInfo Populate(string actionName)
        {
            Command cmd = new Command("getActionInformation");
            cmd.AddParameter("actionName", actionName);
            ActionInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            return result;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }

}
