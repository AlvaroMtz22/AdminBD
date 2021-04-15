using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class DeleteUser : IQuery
    {
        private string m_user;

        public DeleteUser(string user)
        {
            m_user = user;
        }

        public string Run(Database database)
        {
            return null;
        }
    }
}
