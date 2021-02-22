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
        public Table GetTable(string table)
        {
            foreach (Table t in m_tables)
            {
                if (table == t.GetName())
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
        public List<Table> GetList()
        {
            return m_tables;
        }

        public List<TableColumn> select(string table, List<string> selects, CompareWhere compared)
        {
            List<TableColumn> values = null;
            Table t = GetTable(table);
            List<TableColumn> TableColumns=t.GetList();
            List<string> val=null;
            TableColumn column ;

            foreach (TableColumn s in TableColumns) {
                if (compared.GetName().CompareTo(s.GetName())==0) 
                {
                    
                    column = new TableColumn(s.GetName(), s.GetType() );
                    if (compared.GetComparator().CompareTo("=") == 0)
                    {
                        foreach (string value in s.GetList())
                        {
                            if (value.CompareTo(compared.GetName()) == 0) {
                                column.AddValue(value);
                            }

                        }
                    }
                    else if (compared.GetComparator().CompareTo("<") == 0)
                    {
                        foreach (string v in s.GetList())
                        {


                        }
                    }
                    else if (compared.GetComparator().CompareTo("<=") == 0)
                    {
                    }
                    else if (compared.GetComparator().CompareTo(">") == 0)
                    {
                    }
                    else if (compared.GetComparator().CompareTo(">=") == 0)
                    {
                    }
                    values.Add(column);
                }
                
            }
            
            return values;

        }

        public void Delete(string table, CompareWhere compared)
        {
            Table t = GetTable(table);

            List<int> positions = t.CompareValues(compared);
            foreach(TableColumn tc in t.GetList())
            {
                List<string> newList = null;
                for(int i=0;i < tc.GetList().Count; i++)
                {
                    if (!positions.Contains(i))
                    {
                        newList.Add(tc.GetList().ElementAt(i));
                    }
                }
                    tc.SetList(newList);
            }
            
        }     
    }
}
