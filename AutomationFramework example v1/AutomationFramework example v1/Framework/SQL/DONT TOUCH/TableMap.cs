using System;
using System.Reflection;

namespace AutomationFramework_example_v1.Framework.SQL
{
    public class TableMap : ITableMap
    {
       
        public bool HasColumn<T>(T c, string ColumnName) where T : TableMap
        {
            foreach (FieldInfo f in typeof(T).GetFields())
            {
                if (c.columnMatch(ColumnName, f.Name))
                {
                    return true;
                }
            }
            return false;
        }

        public void SetValue<T>(T c, string columnName, object val) where T : TableMap
        {
            Type type = val.GetType();
            foreach (FieldInfo f in typeof(T).GetFields())
            {
                if (c.columnMatch(columnName, f.Name))
                {
                    if (f.FieldType.Equals(typeof(string)))
                    {
                        if (type.Equals(typeof(DBNull)))
                            val = "";
                        f.SetValue(c, val.ToString());
                    }
                    else
                    {
                        if (type.Equals(typeof(DBNull)) && f.FieldType.Equals(typeof(bool)))
                            val = false;
                        if (type.Equals(typeof(DBNull)) && f.FieldType.Equals(typeof(int)))
                            val = 0;
                        f.SetValue(c, val);
                    }
                    
                    break;
                }
            }
        }
    }
    public static class TableExtensions
    {
        public static bool columnMatch<T>(this T c, string ColumnName, string fieldname) where T : TableMap
        {
            return c.GetNameAttribute(fieldname).Equals(ColumnName);
        }

        private static string GetNameAttribute<T>(this T c, string fieldName) where T : TableMap
        {
            var fieldInfo = typeof(T).GetField(fieldName);
            return ((ColumnMap)Attribute.GetCustomAttribute(fieldInfo, typeof(ColumnMap))).Name;
        }
    }
}
