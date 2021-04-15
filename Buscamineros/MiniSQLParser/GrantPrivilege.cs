using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    class GrantPrivilege : IQuery
    {
        private string m_table;
        private string m_privilege;
        private string m_profile;

        public string Table()
        {
            return m_table;
        }

        public string Privilege()
        {
            return m_privilege;
        }

        public string Profile()
        {
            return m_profile;
        }

        public GrantPrivilege(string privilege, string table, string profile)
        {
            m_table = table;
            m_privilege = privilege;
            m_profile = profile;
        }

        public string Run(Database database)
        {
            //return database.GetSecurity().GrantPrivilege();
            return null;
        }
    }
}
