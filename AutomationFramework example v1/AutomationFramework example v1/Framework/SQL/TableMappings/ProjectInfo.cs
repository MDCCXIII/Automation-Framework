﻿using AutomationFramework_example_v1.Framework.Log.LogObjects;
using AutomationFramework_example_v1.Framework.SQL;

namespace AutomationFramework_example_v1.Framework.TableMappings
{
    class ProjectInfo : TableMap
    {
#pragma warning disable 0169
#pragma warning disable 0649
        [ColumnMap("projectId")]
        public int Id;

        [ColumnMap("projectName")]
        public string Name;

        [ColumnMap("projectUrl")]
        public string Url;

        public ProjectInfo Populate(string projectName)
        {
            Command cmd = new Command("getProjectInformation");
            cmd.AddParameter("projectName", projectName);
            ProjectInfo result = this.ExecuteQuery(cmd)[0];
            cmd.Dispose();
            PopulateLogData(result);
            return result;

        }

        private void PopulateLogData(ProjectInfo projectInfo)
        {
            TestLogData.projectUrl = projectInfo.Url;
        }
#pragma warning restore 0169
#pragma warning restore 0649
    }
}
