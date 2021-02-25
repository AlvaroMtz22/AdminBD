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

        public void AddValue(string value)
        {
            m_data.Add(value);
        }

        public string GetColumnType()
        {
            return m_type;
        }

        public string GetName()
        {
            return m_name;
        }

        public List<int> GetPositions(CompareWhere compared)
        {
            List<int> positions = null;

            if (compared.GetComparator().CompareTo("=") == 0)
            {
                for (int i = 0; i < m_data.Count; i++)
                {
                    if (compared.GetName() == m_data.ElementAt(i))
                    {
                        positions.Add(i);
                    }
                };
            }

            else if(compared.GetComparator().CompareTo("<") == 0)
            {
                for (int i = 0; i < m_data.Count; i++)
                {
                    if (compared.GetName().CompareTo(m_data.ElementAt(i)) == -1)
                    {
                        positions.Add(i);
                    }
                };
            }

            else if (compared.GetComparator().CompareTo(">") == 0)
            {
                for (int i = 0; i < m_data.Count; i++)
                {
                    if (compared.GetName().CompareTo(m_data.ElementAt(i)) == 1)
                    {
                        positions.Add(i);
                    }
                };
            }

            return positions;
        }

        public List<string> GetValues(List<int> positions)
        {
            List<string> list = null;
            foreach (int p in positions)
            {
                string value = m_data.ElementAt(p);
                list.Add(value);
            }
            return list;
        }


        public List<string> GetList()
        {
            return m_data;

        }

        public void SetList(List<string> newList)
        {
            m_data = newList;
        }
    }
}
