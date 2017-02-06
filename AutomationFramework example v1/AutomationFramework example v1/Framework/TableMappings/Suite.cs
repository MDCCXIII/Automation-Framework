using AutomationFramework_example_v1.Framework.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class Suite : TableMap
    {
        [ColumnMap("Test Id")]
        public string testId;

        [ColumnMap("Test Name")]
        public string testName;

        [ColumnMap("Execute")]
        public bool execute;

        [ColumnMap("Browser")]
        public string Browser;

        public List<Suite> Populate()
        {
            Command cmd = new Command("getSuiteInformation");
            return this.ExecuteStoredProcedure(cmd);
        }
    }
}
