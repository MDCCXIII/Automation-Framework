using AutomationFramework_example_v1.Framework.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class ActionInfo : TableMap
    {
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
    }
}
