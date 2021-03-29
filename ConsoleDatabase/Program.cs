using Buscamineros;
using System;
using System.IO;
using Buscamineros;
using Buscamineros.MiniSQLParser;

namespace ConsoleDatabase
{
    class Program
    {
        private static System.Security.SecureString contraseña; 
        private static Database database;
        static void Main(string[] args)
        {
            if (database == null) 
            {
                contraseña = new System.Security.SecureString();
                database = new Database("MainDatabase", "AitorUrabain", contraseña); 
            }
            Console.WriteLine("Write the line you want to execute in the DB");
            string linea = Console.ReadLine();
            string queryResult = useDatabase(linea , database);
            Console.WriteLine(queryResult);
            
        }

        private static string useDatabase(string miniSqlSentence, Database database) 
        {
            IQuery IQ = Parser.Parse(miniSqlSentence);
            return IQ.Run(database);

        }
        
    }

}
