using Buscamineros.MiniSQLParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class SelectColumnWhere : IQuery
    {
        private string m_table;
        private List<string> m_columnNames;
        private CompareWhere m_compare;

        public string Table()
        {
            return m_table;
        }
        public List<string> Columns()
        {
            return m_columnNames;
        }

        public CompareWhere Condition()
        {
            return m_compare;
        }

        public SelectColumnWhere(string table, CompareWhere c, string[] columns)
        {
            m_table = table;
            m_compare = c;
            m_columnNames = new List<string>();
            foreach (string cl in columns)
            {
                m_columnNames.Add(cl);
            }

        }
        public string Run(Database database)
        {
            return database.select(m_table,m_columnNames,m_compare).ToString();
        }
    }
}
