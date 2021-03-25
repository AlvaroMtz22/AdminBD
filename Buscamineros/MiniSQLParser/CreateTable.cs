using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class CreateTable : IQuery

    {
        private string m_table;

        public string Table()
        {
            return m_table;
        }

        public CreateTable(string table)
        {
            m_table = table;
        }
        public string Run(Database database)
        {
            return null;
        }
    }
}
