using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class SelectAllWhere : IQuery
    {
        private string m_table;

        public string Table()
        {
            return m_table;
        }

        public SelectAllWhere(string table)
        {
            m_table = table;
        }
        public Table Run(Database database)
        {
            return database.SelectAll(m_table);
        }
    }
    {
    }
}
