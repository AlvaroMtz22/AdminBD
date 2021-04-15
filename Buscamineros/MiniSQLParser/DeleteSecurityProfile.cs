using System;
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

        public string Run(Database database)
        {
            return null;
        }
    }
}
