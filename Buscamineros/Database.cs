﻿using System;
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
        public Table getTable(string table)
        {
            foreach (Table t in m_tables)
            {
                if (table == t.getName())
                {
                    return t;
                }
            }
            return null;
        }

        public string GetName()
        {
            return m_name;
        }
        public List<Table> getList()
        {
            return m_tables;
        }

        public List<string> select(string table, List<string> selects, String column, String name, string comparator)
        {
            List<string> values = null;
            Table t = getTable(table);
            TableColumn tc = t.getColumn(selects.ElementAt(0));
            if (selects.Count == 1)
            {
                if (comparator.Equals('='))
                {
                    List<int> pos = t.CompareValues(selects.ElementAt(0), name);
                    
                    //values = tc.getValues(pos);
                }
            }



            return values;

        }



    }
}
