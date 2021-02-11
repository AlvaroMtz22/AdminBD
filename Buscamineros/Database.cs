using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class Database
    {
        private string m_name;
        private string m_username;
        private System.Security.SecureString m_password;
        private List<Table> m_tables = new List<Table>();

        public Database(string name, string username, System.Security.SecureString
        password)
        {
            m_name = name;
            m_username = username;
            m_password = password;
        }

        public void AddTable(Table table)
        {
            m_tables.Add(table);   
        }

        public void DeleteTable(Table table)
        {
            m_tables.Remove(table);   
        }

        public string GetName()
        {
            return m_name;
        }

        public void select(Table table, List<string> selects, String column, String name)
        {
            /*List<int> positions = table.where(column, name);
            foreach (x in positions)
            {
                x.Show();
            }*/
            return;
        }

    }
}
