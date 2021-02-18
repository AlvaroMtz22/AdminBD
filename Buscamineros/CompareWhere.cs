using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class CompareWhere
    {
        private string m_column;
        private string m_name;
        private string m_comparator;

        public CompareWhere(string column, string name, string comparator)
        {
            m_column = column;
            m_name = name;
            m_comparator = comparator;
        }

        public string GetColumn()
        {
            return m_column;
        }

        public string GetName()
        {
            return m_name;
        }

        public string GetComparator()
        {
            return m_comparator;
        }
    }
}
