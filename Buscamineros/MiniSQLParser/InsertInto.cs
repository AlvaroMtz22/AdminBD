using Buscamineros.MiniSQLParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class InsertInto : IQuery
    {
        private string m_table;
        private List<string> m_values;

        public string Table()
        {
            return m_table;
        }

        public List<string> Values()
        {
            return m_values;
        }

        public InsertInto(string table, string[] columns, string[]values)
        {
            m_table = table;
            m_values = new List<string>();
            foreach(string v in values)
            {
                m_values.Add(v);
            }

        }
        public string Run(Database database)
        {
            return database.InsertInto(m_table, null, m_values);
        }
    }
}
