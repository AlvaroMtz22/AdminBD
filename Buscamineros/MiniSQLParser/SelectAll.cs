using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class SelectAll : IQuery
    {
        private string m_table;

        public string Table()
        {
            return m_table;
        }

        public SelectAll(string table)
        {
            m_table = table;
        }
        public string Run(Database database, User user)
        {
            if (!database.GetList().Contains(database.GetTable(m_table)))
            {
                return Messages.TableDoesNotExist;
            }
            else if(!database.GetSecurity().CheckPrivilege(user, PrivilegeType.Select, m_table))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            return database.SelectAll(m_table,null, user).ToString();
        }
    }
}
