using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AutomationFramework_example_v1.Framework.SQL
{
    static class DAO
    {

        /// <summary>
        /// Takes in a command and populates the class that called this method.
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
                            if (rdr.GetFieldType(rdr.GetOrdinal(column)).Name.Equals("Int32") && rdr[column].ToString().Equals(""))
                            {
                                clazz.SetValue<T>(clazz, column, 0.ToString());
                            }
                            else
                            {
                                clazz.SetValue<T>(clazz, column, rdr[column].ToString());
                            }
                        }
                        result.Add(clazz);
                    }
                }
            }
            return result;
        }

    }
}
