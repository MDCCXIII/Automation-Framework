
using AutomationFramework_example_v1.Framework.SQL;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Reflection;

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

        public static bool columnMatch<T>(this T c, string ColumnName, string fieldname) where T : class 
        {
            return c.GetNameAttribute(fieldname).Equals(ColumnName);
        }

        private static string GetNameAttribute<T>(this T c, string fieldName) where T : class 
        {
            var fieldInfo = typeof(T).GetField(fieldName);
            return ((ColumnMap)Attribute.GetCustomAttribute(fieldInfo, typeof(ColumnMap))).Name;
        }

        public static string DeclaredName(this string field)
        {
            return GetFieldName(() => field);
        }

        private static string GetFieldName<T>(Expression<Func<T>> expr)
        {
            var body = ((MemberExpression)expr.Body);
            return body.Member.Name;
        }
    }
}
