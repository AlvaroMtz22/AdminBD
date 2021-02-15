using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
        }

        public void AddTableColumn(TableColumn tableColumn)
        {
            
        }

        public string getName()
        {
            return m_name;
        }

        public void AddRow(List<string> values)
        {
     
        }
        public List<TableColumn> getList()
        {
            return m_list;
        }


    }
}
