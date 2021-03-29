using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class Messages
    {
        public static string CreateDatabaseSuccess = "Database created";
        public static string OpenDatabaseSuccess = "Database opened";
        public static string DeleteDatabaseSuccess = "Database deleted";
        public static string BackupDatabaseSuccess = "Database backed up";

        public static string CreateTableSuccess = "Table created";
        public static string DeleteTableSuccess = "Table deleted";
        public static string InsertSuccess = "Tuple added";
        public static string TupleDeleteSuccess = "Tuple(s) deleted";
        public static string TupleUpdateSuccess = "Tuple(s) updated";

        public const string Error = "ERROR: ";
        public static string WrongSyntax = Error + "Syntactical error";
        public static string DatabaseDoesNotExist = Error + "Database does not exist";
        public static string TableDoesNotExist = Error + "Table does not exist";
        public static string TableAlreadyExists = Error + "Table exists already";
        public static string ColumnDoesNotExist = Error + "Column does not exist";
        public static string IncorrectDataType = Error + "Incorrect data type";



    }
}
