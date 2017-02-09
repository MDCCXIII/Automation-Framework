using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using AutomationFramework_example_v1.Framework.TableMappings;
using System;

namespace AutomationFramework_example_v1.Framework.SQL
{
    static class DAO
    {
        /// <summary>
        /// NOT WORKING!!!
        /// </summary>
        /// <param name="ProcedureName"></param>
        private static void ExecuteStoredProcedure(string ProcedureName)
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

        /// <summary>
        /// WORKING: takes in a command and populates the class that called this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clazz"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static List<T> ExecuteStoredProcedure<T>(this T clazz, Command command) where T : TableMap
        {
            SqlCommand cmd = command.command;
            cmd = cmd.CheckConnectivity();
            List<T> result = new List<T>();
            using (cmd)
            {
                cmd.Connection.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    List<string> columns = new List<string>();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (clazz.HasColumn<T>(clazz, rdr.GetName(i)))
                        {
                            columns.Add(rdr.GetName(i));
                        }
                    }
                    while (rdr.Read())
                    {
                        clazz = (T)Activator.CreateInstance(typeof(T));
                        foreach (string column in columns)
                        {
                            clazz.SetValue<T>(clazz, column, rdr[column].ToString());
                        }
                        result.Add(clazz);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// NOT WORKING
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="connectionString"></param>
        private static void ExecuteStoredProcedure(SqlCommand cmd, string connectionString)
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
        /// <summary>
        /// NOT WORKING
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="Parameters"></param>
        private static void ExecuteStoredProcedure(string ProcedureName, Dictionary<string, string> Parameters)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[Program.DefaultConnectionStringName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(ProcedureName, conn) { CommandType = CommandType.StoredProcedure })
                {
                    foreach (KeyValuePair<string, string> parameter in Parameters)
                    {
                        command.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                    }
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// NOT WORKING
        /// </summary>
        /// <param name="ProcedureName"></param>
        /// <param name="parameterNames"></param>
        /// <param name="ParameterValues"></param>
        private static void ExecuteStoredProcedure(string ProcedureName, string[] parameterNames, string[] ParameterValues)
        {
            if (parameterNames.Length != ParameterValues.Length)
            {
                throw new System.Exception("ExecuteStoredProcedure: Missmatch number of parameter name/value pairs.");
            }

            string connectionString = ConfigurationManager.ConnectionStrings[Program.DefaultConnectionStringName].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(ProcedureName, conn) { CommandType = CommandType.StoredProcedure })
                {
                    int index = -1;
                    foreach (string parameter in parameterNames)
                    {
                        command.Parameters.Add(new SqlParameter(parameter, ParameterValues[++index]));
                    }
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
