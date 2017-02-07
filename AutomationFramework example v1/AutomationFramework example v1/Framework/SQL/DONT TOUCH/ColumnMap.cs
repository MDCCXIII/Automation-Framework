using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework_example_v1.Framework.SQL
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ColumnMap : Attribute
    {
        string ColumnName;
        object ColumnValue;
        public ColumnMap(string columnName)
        {
            ColumnName = columnName;
        }

        public virtual string Name
        {
            get { return ColumnName; }
        }

        public virtual object Value
        {
            get { return ColumnValue; }
            set { ColumnValue = value; }
        }
        
    }
}
