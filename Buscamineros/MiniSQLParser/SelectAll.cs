﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros.MiniSQLParser
{
    public class SelectAll : IQuery
    {
        private string m_table;

        public string Table()
        {
            return m_table;
        }

        public SelectAll(string table)
        {
            m_table = table;
        }
        public string Run(Database database)
        {
            if (!database.GetList().Contains(database.GetTable(m_table)))
            {
                return Messages.TableDoesNotExist;
            }
            return database.SelectAll(m_table,null).ToString();
        }
    }
}