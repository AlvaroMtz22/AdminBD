using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class Update : IQuery
    {
        private string m_table;
        private CompareWhere m_condition;
        private List<string> m_setColumns;
        private List<string> m_setValues;

        public string Table()
        {
            return m_table;
        }

        public CompareWhere Condition()
        {
            return m_condition;
        }

        public List<string> SetColumns()
        {
            return m_setColumns;
        }

        public List<string> SetValues()
        {
            return m_setValues;
        }

        public Update(string table, CompareWhere condition, List<string> setColumns, List<string> setValues)
        {
            m_table = table;
            m_condition = condition;
            m_setColumns = setColumns;
            m_setValues = setValues;
        }

        public string Run(Database database)
        {
            return database.Update(m_setColumns, m_setValues, m_table, m_condition);
        }
    }
}
