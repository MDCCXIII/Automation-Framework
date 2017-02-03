using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework
{
    static class Extensions
    {
        public static SqlCommand CheckConnectivity(this SqlCommand cmd)
        {
            if (cmd.Connection == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[Program.DefaultConnectionStringName].ConnectionString;
                cmd.Connection = new SqlConnection(connectionString);
            }
            return cmd;
        }

        public static SqlCommand CheckConnectivity(this SqlCommand cmd, string connectionName)
        {
            if (cmd.Connection == null)
            {
                if(ConfigurationManager.AppSettings[connectionName] == null)
                {
                    throw new Exception(connectionName + " is not a valid key in the App.Config");
                }
                string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
                cmd.Connection = new SqlConnection(connectionString);
            }
            return cmd;
        }
    }
}
