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
            m_data.Add(value);
        }


        public string getName()
        {
            return m_name;
        }

        public List<int> getPositions(string value)
        {
            List<int> positions = null;
            for (int i = 0; i < m_data.Count; i++)
            {
                if (value == m_data.ElementAt(i))
                {
                    positions.Add(i);
                }
            };

            return positions;
        }

        public List<string> getValues(List<int> positions)
        {
            List<string> list = null;
            foreach (int p in positions)
            {
                string value = m_data.ElementAt(p);
                list.Add(value);
            }
            return list;
        }


        public List<string> getList()
        {
            return m_data;

        }
    }
}
