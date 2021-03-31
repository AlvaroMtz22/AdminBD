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
            int stopCondition = 0;
            if (database == null) 
            {
                contraseña = new System.Security.SecureString();
                database = new Database("MainDatabase", "AitorUrabain", contraseña); 
            }
            while (stopCondition != 1)
            {
                Console.WriteLine("Write the line you want to execute in the DB");
                string sentence = Console.ReadLine();
                string queryResult = database.RunMiniSqlQuery(sentence);
                Console.WriteLine(queryResult);
                Console.WriteLine("Ponga el numero 1 si desea parar de utilizar la BD");
                stopCondition = Convert.ToInt32(Console.ReadLine());
            }
        }        
    }

}
