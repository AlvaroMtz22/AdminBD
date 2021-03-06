﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class DeleteSecurityProfile : IQuery
    {
        private string m_profile;

        public DeleteSecurityProfile(string profile)
        {
            m_profile = profile;
        }

        public string Profile()
        {
            return m_profile;
        }

        public string Run(Database database, User user)
        {
            return database.GetSecurity().DropSecurityProfile(m_profile, user);
        }
    }
}
