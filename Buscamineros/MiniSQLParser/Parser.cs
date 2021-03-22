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
            const string selectAllWherePattern = @"SELECT \* FROM ([a-zA-Z0-9]+) WHERE ([a-zA-Z0-9]+)([=><])'([a-zA-Z0-9]+)'";
            const string selectColumnsPattern = @"SELECT ([a-zA-Z0-9]+(,[a-zA-Z0-9]+)*) FROM ([a-zA-Z0-9]+)";
            const string selectColumnsWherePattern = @"SELECT ([a-zA-Z0-9]+(,[a-zA-Z0-9]+)*) FROM ([a-zA-Z0-9]+) WHERE ([a-zA-Z0-9]+)([=><])'([a-zA-Z0-9]+)'";
            const string insertIntoPattern = @"INSERT INTO ([a-zA-Z0-9]+) VALUES\(('[a-zA-Z0-9]+')(,'[a-zA-Z0-9]+')*\)";
            

            Match match = Regex.Match(miniSqlSentence, selectAllPattern);
            if(match.Success)
            {
                SelectAll selectAll = new SelectAll(match.Groups[1].Value);
                return selectAll;
            }
            match = Regex.Match(miniSqlSentence, selectAllWherePattern);
            if (match.Success)
            {
                CompareWhere cW = new CompareWhere(match.Groups[2].Value, match.Groups[4].Value, match.Groups[3].Value);
                SelectAllWhere selectAllWhere = new SelectAllWhere(match.Groups[1].Value, cW);
                return selectAllWhere;
            }
            match = Regex.Match(miniSqlSentence, selectColumnsPattern);
            if (match.Success)
            {
                string[] columnNames = match.Groups[1].Value.Split(',');
                SelectColumns selectColumns = new SelectColumns(match.Groups[3].Value,columnNames);
                return selectColumns;
            }
            match = Regex.Match(miniSqlSentence, selectColumnsWherePattern);
            if (match.Success)
            {
                string[] columnNames = match.Groups[1].Value.Split(',');
                CompareWhere cW = new CompareWhere(match.Groups[4].Value, match.Groups[6].Value, match.Groups[5].Value);
                SelectColumnWhere selectColumnWhere = new SelectColumnWhere(match.Groups[3].Value, cW, columnNames);
                return selectColumnWhere;
            }
            match = Regex.Match(miniSqlSentence, insertIntoPattern);
            if (match.Success)
            {
                string temp = match.Groups[2].Value.Replace("'","");
                string[] columnNames = temp.Split(',');
                InsertInto insertInto = new InsertInto(match.Groups[1].Value, null,columnNames);
                return insertInto;
            }

            return null;
        }
    }
}
