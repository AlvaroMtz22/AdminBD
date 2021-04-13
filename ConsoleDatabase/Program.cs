using Buscamineros;
using System;
using System.IO;
using Buscamineros.MiniSQLParser;
using System.Diagnostics;
using System.Threading;

namespace ConsoleDatabase
{
    class Program
    {
        private static System.Security.SecureString password; 
        private static Database database;
        static void Main(string[] args)
        {
            using (TextWriter writer = File.CreateText("output-file.text"))
            {
                string[] FileLines = File.ReadAllLines("input-file.txt");
                int numtest = 0;
                TimeSpan totalTime = new(0);
                for (int i = 0; i < FileLines.Length; i++)
                {
                    
                    if (i == 0 || FileLines[i] == "")
                    {
                        if (i != 0)
                        {
                            Console.WriteLine("TOTAL TIME: "+totalTime.TotalSeconds);
                            totalTime = new(0);
                            Console.WriteLine("");
                            writer.WriteLine("");
                        }
                        if (FileLines[i] == "")
                        {
                            i++;
                        }
                        numtest++;
                        Console.WriteLine("# Test " + (numtest));
                        writer.WriteLine("# Test " + (numtest));
                        password = new System.Security.SecureString();
                        database = new Database("Database", "User", password);

                        string sentence = FileLines[i];

                        DateTime start = DateTime.Now;
                        string queryResult = database.RunMiniSqlQuery(sentence);
                        DateTime end = DateTime.Now;
                        TimeSpan ts = (end - start);
                        totalTime += ts;
                        Console.WriteLine(queryResult + " (" + ts.TotalSeconds + "s)");
                        writer.WriteLine(queryResult);

                    }
                    else
                    {
                        string sentence = FileLines[i];
                    
                        DateTime start = DateTime.Now;
                        string queryResult = database.RunMiniSqlQuery(sentence);
                        DateTime end = DateTime.Now;
                        TimeSpan ts = (end - start);
                        totalTime += ts;
                        Console.WriteLine(queryResult + " (" + ts.TotalSeconds + "s)");
                        writer.WriteLine(queryResult);
                    }
                }
                Console.WriteLine("TOTAL TIME: " + totalTime.TotalSeconds);

            }
        } 
    }

}
