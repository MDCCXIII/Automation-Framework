using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework.SQL
{
    interface ITableMap
    {
        bool HasColumn<T>(string ColumnName) where T : class;

        void SetValue<T>(object val);
    }
}
