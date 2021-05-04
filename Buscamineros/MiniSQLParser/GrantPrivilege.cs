using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class GrantPrivilege : IQuery
    {
        private string m_table;
        private PrivilegeType m_privilege;
        private string m_profile;

        public string Table()
        {
            return m_table;
        }

        public PrivilegeType Privilege()
        {
            return m_privilege;
        }

        public string Profile()
        {
            return m_profile;
        }

        public GrantPrivilege(PrivilegeType privilege, string table, string profile)
        {
            m_table = table;
            m_privilege = privilege;
            m_profile = profile;
        }

        public string Run(Database database, User user)
        {
            if (database.GetList().Contains(database.GetTable(m_table)))
            {
                return database.GetSecurity().GrantPrivilege(m_privilege,m_table,m_profile, user);
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
        }
    }
}
