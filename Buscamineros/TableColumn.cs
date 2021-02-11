using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class TableColumn
    {
        private List<string> m_data;
        private string m_name;
        private string m_type;

        public TableColumn(string name, string type)
        {
            m_name = name;
            m_type = type;
        }

        public void addValue(string value)
        {
            
        }

        public string getName()
        {
            return m_name;
        }

    }
}
