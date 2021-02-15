using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buscamineros
{
    public class Table
    {
        private string m_name;
        private List<TableColumn> m_list = new List<TableColumn>();

        public Table(string name, List<TableColumn> tableColumns)
        {
            m_name = name;
            m_list = tableColumns;
        }

        public void DeleteTableColumn(TableColumn tableColumn)
        {
            m_list.Remove(tableColumn);
        }

        public void AddTableColumn(TableColumn tableColumn)
        {
            m_list.Add(tableColumn);
        }

        public string getName()
        {
            return m_name;
        }

        public void AddRow(List<string> values)
        {
            if (values.Count == m_list.Count)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    //string value = values.get(i);

                }
            }

        }

        public List<int> CompareValues(string column, string name)
        {
            List<int> positions = null;
            foreach(TableColumn x in m_list)
            {
                if(x.getName()==(column))
                {
                    //positions = (x.getPositions(name));
                }
            }

            return positions; 

        }
        public List<TableColumn> getList()
        {
            return m_list;
        }

        public TableColumn getColumn(string column)
        {
            foreach (TableColumn c in m_list)
            {
                if (c.getName() == column)
                {
                    return c;
                }
            }
            return null;
        }


    }
}
