using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Buscamineros.MiniSQLParser
{
    public static class Parser
    {
        public static IQuery Parse(string miniSqlSentence)
        {
            const string selectAllPattern = @"SELECT \* FROM ([a-zA-Z0-9]+)";
            const string selectColumnsPattern = @"SELECT ([a-zA-Z0-9,]+) FROM ([a-zA-Z0-9]+)";
            const string deletePattern = @"DELETE FROM ([a-zA-Z0-9]+) WHERE ([a-zA-Z]+)([=><])'([a-zA-Z0-9]+)'";
            const string updatePattern = @"UPDATE ([a-zA-Z0-9]+) SET ([a-zA-Z0-9]+='[a-zA-Z0-9]+'(,[a-zA-Z0-9]+='[a-zA-Z0-9]+')*) WHERE ([a-zA-Z]+)([=><])'([a-zA-Z0-9]+)'";

            Match match = Regex.Match(miniSqlSentence, selectAllPattern);
            if(match.Success)
            {
                SelectAll selectAll = new SelectAll(match.Groups[1].Value);
                return selectAll;
            }
            match = Regex.Match(miniSqlSentence, selectColumnsPattern);
            if (match.Success)
            {
                string[] columnNames = match.Groups[1].Value.Split(',');
                SelectColumns selectColumns = new SelectColumns(match.Groups[2].Value,columnNames);
                return selectColumns;
            }
            match = Regex.Match(miniSqlSentence, deletePattern);
            if (match.Success)
            {
                CompareWhere cW = new CompareWhere(match.Groups[2].Value, match.Groups[4].Value, match.Groups[3].Value);
                Delete delete = new Delete(match.Groups[1].Value, cW);
                return delete;
            }
            match = Regex.Match(miniSqlSentence, updatePattern);
            if (match.Success)
            {
                CompareWhere cW = new CompareWhere(match.Groups[2].Value, match.Groups[4].Value, match.Groups[3].Value);
                string[] columnNames = match.Groups[1].Value.Split(',');
                Update update = new Update(match.Groups[1].Value, columnNames);
                return update;
            }
            return null;
        }
    }
}
