using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class CreateSecurityProfile : IQuery
    {
        private string m_profile;

        public CreateSecurityProfile(string profile)
        {
            m_profile = profile;
        }

        public string Profile()
        {
            return m_profile;
        }

        public string Run(Database database)
        {
            return null;    
        }
    }
}
