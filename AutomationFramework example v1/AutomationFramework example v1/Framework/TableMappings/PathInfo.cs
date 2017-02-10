using AutomationFramework_example_v1.Framework.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class PathInfo : TableMap
    {
        [ColumnMap("pathId")]
        public int pathId;

        [ColumnMap("pathName")]
        public string pathName;

        [ColumnMap("path")]
        public string path;

        [ColumnMap("pathType")]
        public string pathType;

        [ColumnMap("projectName")]
        public string projectName;

        public PathInfo Populate(string pathName, string projectName)
        {
            Command cmd = new Command("getPathInformation");
            cmd.AddParameter("pathName", pathName);
            cmd.AddParameter("projectName", projectName);
            PathInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            return result;
        }
    }
}
