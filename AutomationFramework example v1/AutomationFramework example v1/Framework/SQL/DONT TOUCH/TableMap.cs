using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            bool x = false;
            int y = 0;
            if(Boolean.TryParse((string)val, out x))
            {
                val = x;
            }
            else if(Int32.TryParse((string)val, out y))
            {
                val = y;
            }
            
            foreach (FieldInfo f in typeof(T).GetFields())
            {
                if (c.columnMatch(columnName, f.Name))
                {
                    f.SetValue(c, val);
                    break;
                }
            }
        }
    }
}
