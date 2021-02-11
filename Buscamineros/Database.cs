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
            
        }

        public void DeleteTable(Table table)
        {
            
        }

        public string GetName()
        {
            return null;
        }


    }
}
