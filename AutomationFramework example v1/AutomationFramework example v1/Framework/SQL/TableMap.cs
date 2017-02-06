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
        public bool HasColumn<T>(string ColumnName) where T : class
        {
            foreach (FieldInfo f in typeof(T).GetFields())
            {
                if (this.columnMatch(ColumnName, f.Name))
                {
                    return true;
                }
            }
            return false;
        }

        public void SetValue<T>(object val)
        {
            throw new NotImplementedException();
        }
    }
}
