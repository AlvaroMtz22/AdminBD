using System;
using Buscamineros;
using Buscamineros.MiniSQLParser;

namespace ConsoleDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static string useDatabase(string miniSqlSentence, Database database) 
        {
            IQuery IQ = Parser.Parse(miniSqlSentence);
            return IQ.Run(database);

        }
        
    }

}
