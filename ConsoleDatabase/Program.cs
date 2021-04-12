using Buscamineros;
using System;
using System.IO;
using Buscamineros.MiniSQLParser;

namespace ConsoleDatabase
{
    class Program
    {
        private static System.Security.SecureString password; 
        private static Database database;
        static void Main(string[] args)
        {

            string[] FileLines = File.ReadAllLines("input-file.txt");
            int numtest = 0;
            for (int i = 0; i < FileLines.Length; i++)
            {
                if (i == 0 || FileLines[i] == "")
                {
                    if (i != 0)
                    {
                        Console.WriteLine("");
                    }
                    if(FileLines[i] == "")
                    {
                        i++;
                    }
                    numtest++;
                    Console.WriteLine("# Test " + (numtest));
                    password = new System.Security.SecureString();
                    database = new Database("Database", "User", password);

                    string sentence = FileLines[i];
                    ProcessSentence(sentence);

                }
                else
                {
                    string sentence = FileLines[i];
                    ProcessSentence(sentence);
                }
            }


        } 
        //using(TextWriter writer = File.CreateText("output-file.text){writer.WriteLine(...)}

        public static void ProcessSentence(string sentence)
        {
            
            string queryResult = database.RunMiniSqlQuery(sentence);
            Console.WriteLine(queryResult);
        }
    }

}
