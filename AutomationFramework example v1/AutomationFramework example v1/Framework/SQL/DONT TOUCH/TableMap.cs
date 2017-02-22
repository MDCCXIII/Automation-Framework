using System;
using System.Data.SqlClient;
using System.Reflection;

namespace AutomationFramework_example_v1.Framework.SQL
{
    class TableMap : ITableMap
    {
       
        public bool HasColumn<T>(T c, string ColumnName) where T : class
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

        public void SetValue<T>(T c, string columnName, object val) where T : class
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
}
