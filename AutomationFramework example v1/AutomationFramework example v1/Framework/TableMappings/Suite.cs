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
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("Id")]
        public int Id;

        [ColumnMap("testName")]
        public string testName;

        [ColumnMap("Execute")]
        public bool execute;

        [ColumnMap("testedBrowser")]
        public string browser;

        [ColumnMap("projectName")]
        public string projectName;

        public List<Suite> Populate()
        {
            Command cmd = new Command("getSuiteInformation");
            List<Suite> result = this.ExecuteStoredProcedure(cmd);
            cmd.Dispose();
            return result;

        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
