using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class CreateTable : IQuery
    {
        private string m_tableName;
        private List<TableColumn> m_columns;

        public string TableName()
        {
            return m_tableName;
        }

        public List<TableColumn> Columns()
        {
            return m_columns;
        }

        public CreateTable(string table, List<TableColumn> columns)
        {
            m_tableName = table;
            m_columns = columns;
        }
        public string Run(Database database)
        {
            return null;
        }
    }
}
