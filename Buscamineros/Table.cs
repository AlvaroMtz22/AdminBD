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

        public string GetName()
        {
            return m_name;
        }

        public void AddRow(List<string> values)
        {
            if (values.Count == m_list.Count)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    string value = values.ElementAt(i);
                    TableColumn tc = GetList().ElementAt(i);
                    tc.AddValue(value);
                }
            }

        }

        public List<int> CompareValues(CompareWhere compared)
        {
            List<int> positions = null;
            foreach(TableColumn x in m_list)
            {
                if(x.GetName()==(column))
                {
                    positions = (x.GetPositions(name));
                }
            }

            return positions; 

        }
        public List<TableColumn> GetList()
        {
            return m_list;
        }

        public TableColumn GetColumn(string column)
        {
            foreach (TableColumn c in m_list)
            {
                if (c.GetName() == column)
                {
                    return c;
                }
            }
            return null;
        }


    }
}
