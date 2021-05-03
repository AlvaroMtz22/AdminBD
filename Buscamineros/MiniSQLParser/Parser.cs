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
            const string selectAllPattern = @"SELECT \* FROM ([a-zA-Z0-9]+);";
            const string deletePattern = @"DELETE FROM ([a-zA-Z0-9]+) WHERE ([a-zA-Z]+)([=><])('{0,1})([a-zA-Z0-9-\.\s]+)\4;";
            const string updatePattern = @"UPDATE ([a-zA-Z0-9]+) SET ([a-zA-Z0-9]+=('{0,1})[a-zA-Z0-9\.\s-]+\3(,[a-zA-Z0-9]+=('{0,1})[a-zA-Z0-9\.\s-]+\5)*) WHERE ([a-zA-Z]+)([=><])('{0,1})([a-zA-Z0-9-\.\s]+)\8;";
            const string selectAllWherePattern = @"SELECT \* FROM ([a-zA-Z0-9]+) WHERE ([a-zA-Z]+)([=><])('{0,1})([a-zA-Z0-9-\.\s]+)\4;";
            const string selectColumnsPattern = @"SELECT ([a-zA-Z0-9]+(,[a-zA-Z0-9]+)*) FROM ([a-zA-Z0-9]+);";
            const string selectColumnsWherePattern = @"SELECT ([a-zA-Z0-9]+(,[a-zA-Z0-9]+)*) FROM ([a-zA-Z0-9]+) WHERE ([a-zA-Z0-9]+)([=><])('{0,1})([a-zA-Z0-9-\.\s]+)\6;";
            const string insertIntoPattern = @"INSERT INTO ([a-zA-Z0-9]+) VALUES \(((('{0,1})[a-zA-Z0-9\.\s-]+\4)+(,('{0,1})[a-zA-Z0-9\.\s-]+\6)*)\);";
            const string dropTablePattern = @"DROP TABLE ([a-zA-Z0-9]+);";
            const string createTablePattern = @"CREATE TABLE ([a-zA-Z0-9]+) \(((([a-zA-Z0-9]+) (INT|DOUBLE|TEXT))+((,([a-zA-Z0-9]+) (INT|DOUBLE|TEXT))*))\);";
            const string grantPrivelegePattern = @"GRANT (INSERT|DELETE|SELECT|UPDATE) ON ([a-zA-Z0-9]+) TO ([a-zA-Z0-9-_\.\s]+);";
            const string revokePrivelegePattern = @"REVOKE (INSERT|DELETE|SELECT|UPDATE) ON ([a-zA-Z0-9]+) TO ([a-zA-Z0-9-_\.\s]+);";
            const string addUserPattern = @"ADD USER \('([a-zA-Z0-9]+)','([a-zA-Z0-9]+)',([a-zA-Z0-9]+)\);";
            const string createSecurityProfilePattern = @"CREATE SECURITY PROFILE ([a-zA-Z0-9-_\.\s]+);";
            const string deleteSecurityProfilePattern = @"DROP SECURITY PROFILE ([a-zA-Z0-9-_\.\s]+);";
            const string deleteUserPattern = @"DELETE USER ([a-zA-Z0-9-_\.\s]+);";



            Match match = Regex.Match(miniSqlSentence, selectAllWherePattern);
            if (match.Success)
            {
                CompareWhere cW = new CompareWhere(match.Groups[2].Value, match.Groups[5].Value, match.Groups[3].Value);
                SelectAllWhere selectAllWhere = new SelectAllWhere(match.Groups[1].Value, cW);
                return selectAllWhere;
            }
            match = Regex.Match(miniSqlSentence, selectAllPattern);
            if (match.Success)
            {
                SelectAll selectAll = new SelectAll(match.Groups[1].Value);
                return selectAll;
            }
            match = Regex.Match(miniSqlSentence, selectColumnsWherePattern);
            if (match.Success)
            {
                string[] columnNames = match.Groups[1].Value.Split(',');
                CompareWhere cW = new CompareWhere(match.Groups[4].Value, match.Groups[7].Value, match.Groups[5].Value);
                SelectColumnWhere selectColumnWhere = new SelectColumnWhere(match.Groups[3].Value, cW, columnNames);
                return selectColumnWhere;
            }
            match = Regex.Match(miniSqlSentence, selectColumnsPattern);
            if (match.Success)
            {
                string[] columnNames = match.Groups[1].Value.Split(',');
                SelectColumns selectColumns = new SelectColumns(match.Groups[3].Value,columnNames);
                return selectColumns;
            }
            match = Regex.Match(miniSqlSentence, deletePattern);
            if (match.Success)
            {
                CompareWhere cW = new CompareWhere(match.Groups[2].Value, match.Groups[5].Value, match.Groups[3].Value);
                Delete delete = new Delete(match.Groups[1].Value, cW);
                return delete;
            }
            match = Regex.Match(miniSqlSentence, updatePattern);
            if (match.Success)
            {
                CompareWhere cW = new CompareWhere(match.Groups[6].Value, match.Groups[9].Value, match.Groups[7].Value);

                List<string> setColumns = new List<string>();
                List<string> setValues = new List<string>();

                string set = match.Groups[2].Value.Replace("'","");
                string[] setElements = set.Split(',');
                for(int i=0; i<setElements.Length; i++)
                {
                    string[] columnAndValue = setElements[i].Split('=');
                    string column = columnAndValue[0];
                    string value = columnAndValue[1];

                    setColumns.Add(column);
                    setValues.Add(value);
                }
                Update update = new Update(match.Groups[1].Value, cW, setColumns, setValues);
                return update;
            }
            match = Regex.Match(miniSqlSentence, insertIntoPattern);
            if (match.Success)
            {
                string temp = match.Groups[2].Value.Replace("'","");
                string[] columnNames = temp.Split(',');
                InsertInto insertInto = new InsertInto(match.Groups[1].Value, null,columnNames);
                return insertInto;
            }
            match = Regex.Match(miniSqlSentence, createTablePattern);
            if (match.Success)
            {
                List<TableColumn> columns = new List<TableColumn>();

                string tableName = match.Groups[1].Value;
                string[] temp = match.Groups[2].Value.Split(',');

                for (int i = 0; i < temp.Length; i++)
                {
                    string[] columnAndValue = temp[i].Split(' ');
                    string column = columnAndValue[0];
                    string value = columnAndValue[1];

                    TableColumn tc = new TableColumn(column, value);
                    columns.Add(tc);
                }

                CreateTable createTable = new CreateTable(tableName, columns);
                return createTable;
            }
            match = Regex.Match(miniSqlSentence, dropTablePattern);
            if (match.Success)
            {
                DropTable dropTable = new DropTable(match.Groups[1].Value);
                return dropTable;
            }
            match = Regex.Match(miniSqlSentence, grantPrivelegePattern);
            if (match.Success)
            {
                GrantPrivilege grantPrivilege = new GrantPrivilege(StringToPrivilegeType(match.Groups[1].Value), match.Groups[2].Value, match.Groups[3].Value);
                return grantPrivilege;
            }
            match = Regex.Match(miniSqlSentence, revokePrivelegePattern);
            if (match.Success)
            {
                RevokePrivilege revokePrivilege = new RevokePrivilege(StringToPrivilegeType(match.Groups[1].Value), match.Groups[2].Value, match.Groups[3].Value);
                return revokePrivilege;
            }
            match = Regex.Match(miniSqlSentence, addUserPattern);
            if (match.Success)
            {
                System.Security.SecureString password = new System.Security.SecureString();

                foreach (char c in match.Groups[2].Value)
                {
                    password.AppendChar(c);
                }
                    
                AddUser addUser = new AddUser(match.Groups[1].Value, password, match.Groups[3].Value);
                return addUser;
            }
            match = Regex.Match(miniSqlSentence, createSecurityProfilePattern);
            if (match.Success)
            {
                CreateSecurityProfile createProfile = new CreateSecurityProfile(match.Groups[1].Value);
                return createProfile;
            }
            match = Regex.Match(miniSqlSentence, deleteSecurityProfilePattern);
            if (match.Success)
            {
                DeleteSecurityProfile deleteProfile = new DeleteSecurityProfile(match.Groups[1].Value);
                return deleteProfile;
            }
            match = Regex.Match(miniSqlSentence, deleteUserPattern);
            if (match.Success)
            {
                DeleteUser deleteUser = new DeleteUser(match.Groups[1].Value);
                return deleteUser;
            }


            return null;
        }

        public static PrivilegeType StringToPrivilegeType(string privilege)
        {
            switch (privilege)
            {
                case "SELECT":
                    return PrivilegeType.Select;
                case "INSERT":
                    return PrivilegeType.Insert;
                case "UPDATE":
                    return PrivilegeType.Update;
                case "DELETE":
                    return PrivilegeType.Delete;
            }
            return PrivilegeType.Delete;
        }
    }
}