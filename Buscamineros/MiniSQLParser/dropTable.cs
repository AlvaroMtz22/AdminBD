using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class DropTable : IQuery
    {
        private string m_table;

        public string Table()
        {
            return m_table;
        }

        public DropTable(string table)
        {
            m_table = table;
        }
        public string Run(Database database, User user)
        {
            return database.DeleteTable(m_table, user);
        }
    }
}
