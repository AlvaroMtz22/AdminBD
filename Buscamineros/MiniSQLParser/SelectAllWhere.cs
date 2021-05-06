using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class SelectAllWhere : IQuery
    {
        private string m_table;
        private CompareWhere m_compare;

        public string Table()
        {
            return m_table;
        }

        public CompareWhere Condition()
        {
            return m_compare;
        }

        public SelectAllWhere(string table, CompareWhere c)
        {
            m_table = table;
            m_compare = c;
        }
        public string Run(Database database, User user)
        {
            if (!database.GetList().Contains(database.GetTable(m_table)))
            {
                return Messages.TableDoesNotExist;
            }
            else if (!database.GetSecurity().CheckPrivilege(user, PrivilegeType.Select, m_table))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            return database.SelectAll(m_table,m_compare, user).ToString();
        }
    }
    
    }

