using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class Delete : IQuery
    {
        private string m_table;
        private CompareWhere m_condition;

        public string Table()
        {
            return m_table;
        }

        public CompareWhere Condition()
        {
            return m_condition;
        }

        public Delete(string table, CompareWhere condition)
        {
            m_table = table;
            m_condition = condition;
        }
        public Table Run(Database database)
        {
            return database.Delete(m_table, m_condition);
        }
    }
}
