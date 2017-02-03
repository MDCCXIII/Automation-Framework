using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;

namespace AutomationFramework_example_v1.Framework.SQL
{
    class DAO
    {
        public static void ExecuteStoredProcedure(string ProcedureName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[Program.DefaultConnectionStringName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(ProcedureName, conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    
                }
            }
        }

        public static DataSet ExecuteStoredProcedure(Command command)
        {
            SqlCommand cmd = command.command;
            cmd.CheckConnectivity();

            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.TableMappings.Add("Suite", "Suite");
                adapter.Fill(ds);
            }
            return ds;
        }

        public static void ExecuteStoredProcedure(SqlCommand cmd, string connectionString)
        {
            if (cmd.Connection.ConnectionString == null)
            {
                cmd.Connection = new SqlConnection(connectionString);
            }
            using (cmd)
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public static void ExecuteStoredProcedure(string ProcedureName, Dictionary<string, string> Parameters)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[Program.DefaultConnectionStringName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(ProcedureName, conn) { CommandType = CommandType.StoredProcedure })
                {
                    foreach(KeyValuePair<string, string> parameter in Parameters)
                    {
                        command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                    }
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ExecuteStoredProcedure(string ProcedureName, string[] parameterNames, string[] ParameterValues)
        {
            if(parameterNames.Length != ParameterValues.Length)
            {
                throw new System.Exception("ExecuteStoredProcedure: Missmatch number of parameter name/value pairs.");
            }

            string connectionString = ConfigurationManager.ConnectionStrings[Program.DefaultConnectionStringName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(ProcedureName, conn) { CommandType = CommandType.StoredProcedure })
                {
                    int index = -1;
                    foreach(string parameter in parameterNames) { 
                        command.Parameters.Add(new SqlParameter(parameter, ParameterValues[++index]));
                    }
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        } 
    }
}
