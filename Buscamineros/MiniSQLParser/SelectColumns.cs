using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class SelectColumns : IQuery
    {
        private string m_table;
        private List<string> m_columnNames;

        public string Table()
        {
            return m_table;
        }
        public List<string> Columns()
        {
            return m_columnNames;
        }
        public SelectColumns(string table, string [] columnNames)
        {
            m_table = table;
            m_columnNames = new List<string>();
            foreach (string c in columnNames)
            {
                m_columnNames.Add(c);
            }
        }

        public string Run(Database database, User user)
        {
            if(!database.GetSecurity().CheckPrivilege(user, PrivilegeType.Select, m_table))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            else if (!database.GetList().Contains(database.GetTable(m_table)))
            {
                return Messages.TableDoesNotExist;
            }
            else 
            {
                foreach(string tc in m_columnNames)
                {
                    if(database.GetTable(m_table).GetColumn(tc) == null)
                    {
                        return Messages.TableDoesNotExist;
                    }
                }
            }
            return database.Select(m_table,m_columnNames,null, user).ToString();
        }
    }
}
