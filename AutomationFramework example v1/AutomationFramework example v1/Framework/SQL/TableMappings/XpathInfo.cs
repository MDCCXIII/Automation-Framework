using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class XpathInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("pathId")]
        public int pathId;

        [ColumnMap("pathName")]
        public string pathName;

        [ColumnMap("path")]
        public string path;

        [ColumnMap("projectName")]
        public string projectName;

        public XpathInfo Populate(string pathName, string projectName)
        {
            Command cmd = new Command("getXpathInformation");
            cmd.AddParameter("pathName", pathName);
            cmd.AddParameter("projectName", projectName);
            XpathInfo result = this.ExecuteStoredProcedure(cmd)[0];
            cmd.Dispose();
            PopulateLogData(result);
            return result;
        }

        private void PopulateLogData(XpathInfo xpathInfo)
        {
            TestLogData.xpath = xpathInfo.path;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
