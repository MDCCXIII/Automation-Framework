using AutomationFramework_example_v1.Framework.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class KeywordInfo : TableMap
    {
        [ColumnMap("Id")]
        public int Id;

        [ColumnMap("keywordName")]
        public string name;

        [ColumnMap("requiredParameters")]
        public string requiredParameters;

        [ColumnMap("optionalParameters")]
        public string optionalParameters;

        [ColumnMap("applicableProject")]
        public string applicableProject;

        [ColumnMap("keywordDescription")]
        public string description;

        public KeywordInfo Populate(string keywordName, string projectName)
        {
            Command cmd = new Command("getKeywordInformation");
            cmd.AddParameter("keywordName", keywordName);
            cmd.AddParameter("projectName", projectName);
            KeywordInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            return result;

        }
    }
}
