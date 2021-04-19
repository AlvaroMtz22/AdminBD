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
            using (TextWriter writer = File.CreateText("output-file.txt"))
            {
                string[] FileLines = File.ReadAllLines("input-file.txt");
                int numtest = 0;
                TimeSpan totalTime = new(0);
                for (int i = 0; i < FileLines.Length; i++)
                {
                    //Execution of first line or empty line
                    if (i == 0 || FileLines[i] == "")
                    {
                        if (i != 0)
                        {
                            //writes at the output file the total time of each test
                            Console.WriteLine("TOTAL TIME: "+totalTime.TotalSeconds + "s");
                            writer.WriteLine("TOTAL TIME: "+totalTime.TotalSeconds + "s");
                            totalTime = new(0);
                            //writes a empty line after the total time 
                            Console.WriteLine("");
                            writer.WriteLine("");
                        }
                        if (FileLines[i] == "")
                        {
                            i++;
                        }
                        //starts with the number of test
                        numtest++;
                        Console.WriteLine("# Test " + (numtest));
                        writer.WriteLine("# Test " + (numtest));

                        //for each test a different database is opened
                        password = new System.Security.SecureString();
                        database = new Database("Database", "User", password);

                        string sentence = FileLines[i];
                        //Captures the time of the RunMiniSQLQUery method execution
                        DateTime start = DateTime.Now;
                        string queryResult = database.RunMiniSqlQuery(sentence);
                        DateTime end = DateTime.Now;
                        TimeSpan ts = (end - start);
                        // ts gets the time of the execution
                        //total Time of each test of the file that was passed as parameter
                        totalTime += ts;
                        Console.WriteLine(queryResult + " (" + ts.TotalSeconds + "s)");
                        writer.WriteLine(queryResult + " (" + ts.TotalSeconds + "s)");

                    }
                    else
                    {
                        //catches the line to execute
                        string sentence = FileLines[i];
                        //Captures the time of the RunMiniSQLQUery method execution
                        DateTime start = DateTime.Now;
                        string queryResult = database.RunMiniSqlQuery(sentence);
                        DateTime end = DateTime.Now;
                        TimeSpan ts = (end - start);
                        totalTime += ts;
                        // ts gets the time of the execution
                        //total Time of each test of the file that was passed as parameter
                        Console.WriteLine(queryResult + " (" + ts.TotalSeconds + "s)");
                        writer.WriteLine(queryResult + " (" + ts.TotalSeconds + "s)");
                    }
                }
                //As the last line of the file has not an empty space,
                // we put the time with these lines
                Console.WriteLine("TOTAL TIME: " + totalTime.TotalSeconds + "s");
                writer.WriteLine("TOTAL TIME: " + totalTime.TotalSeconds + "s");
            }
        } 
    }

}
