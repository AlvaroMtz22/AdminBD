﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class AddUser : IQuery
    {
        private string m_user;
        private System.Security.SecureString m_password;
        private string m_profile;

        public string User()
        {
            return m_user;
        }

        public System.Security.SecureString Password()
        {
            return m_password;
        }

        public string Profile()
        {
            return m_profile;
        }

        public AddUser(string user, System.Security.SecureString password, string profile)
        {
            m_user = user;
            m_password = password;
            m_profile = profile;
        }

        public string Run(Database database)
        {
            return database.GetSecurity().AddUser(m_user, m_password, m_profile);
        }
    }
}
