using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class SelectColumns : IQuery
    {
        private string m_table;
        private List<string> m_columnNames;


        public string Table()
        {
            return m_table;
        }
        public List<string> Column()
        {
            return m_columnNames;
        }
        public SelectColumns(string table, string [] columnNames)
        {
            m_table = table;
            foreach(string c in columnNames)
            {
                m_columnNames.Add(c);
            }
        }

        public Table Run(Database database)
        {
            return database.select(m_table,m_columnNames,null);
        }
    }
}
