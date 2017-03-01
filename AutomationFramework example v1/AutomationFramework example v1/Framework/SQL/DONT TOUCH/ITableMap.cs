namespace AutomationFramework_example_v1.Framework.SQL
{
    interface ITableMap
    {
        bool HasColumn<T>(T c, string ColumnName) where T : TableMap;

        void SetValue<T>(T c, string columnName, object val) where T : TableMap;
    }
}
