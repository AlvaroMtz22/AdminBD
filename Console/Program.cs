using Buscamineros;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdminBDConsole
{
    class Program
    {
        private static string password;
        private static Database database;
        static void Main(string[] args)
        {
            using (TextWriter writer = File.CreateText("output-file.txt"))
            {
                string[] FileLines = File.ReadAllLines("input-file.txt");
                int numtest = 0;
                TimeSpan totalTime = TimeSpan.FromSeconds(0);
                List<Database> databaseList = new List<Database>();
                string username = null;
                for (int i = 0; i < FileLines.Length; i++)
                {
                    //Execution of first line or empty line
                    if (i == 0 || FileLines[i] == "")
                    {
                        if (i != 0)
                        {
                            //writes at the output file the total time of each test
                            Console.WriteLine("TOTAL TIME: " + totalTime.TotalSeconds + "s");
                            writer.WriteLine("TOTAL TIME: " + totalTime.TotalSeconds + "s");
                            totalTime = TimeSpan.FromSeconds(0);
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

                        string sentence = FileLines[i];

                        DateTime start = DateTime.Now;

                        string[] splittedSentence = sentence.Split(',');
                        string databaseName = splittedSentence[0];
                        username = splittedSentence[1];
                        string password = splittedSentence[2];

                        bool exists = false;
                        foreach (Database db in databaseList)
                        {
                            if (db.GetName() == databaseName)
                            {
                                exists = true;
                            }
                        }
                        if (!exists)
                        {
                            database = new Database(databaseName, username, password);
                            databaseList.Add(database);

                            if (!database.GetSecurity().CheckPassword(username, password))
                            {
                                DateTime end = DateTime.Now;
                                TimeSpan ts = (end - start);
                                Console.WriteLine(Messages.SecurityIncorrectLogin + " (" + ts.TotalSeconds + "s)");
                                writer.WriteLine(Messages.SecurityIncorrectLogin + " (" + ts.TotalSeconds + "s)");
                            }
                            else
                            {
                                DateTime end = DateTime.Now;
                                TimeSpan ts = (end - start);
                                Console.WriteLine(Messages.CreateDatabaseSuccess + " (" + ts.TotalSeconds + "s)");
                                writer.WriteLine(Messages.CreateDatabaseSuccess + " (" + ts.TotalSeconds + "s)");
                            }
                        }
                        else
                        {
                            foreach (Database db in databaseList)
                            {
                                if (db.GetName() == databaseName)
                                {
                                    database = db;
                                }
                            }
                            if (!database.GetSecurity().CheckPassword(username, password))
                            {
                                DateTime end = DateTime.Now;
                                TimeSpan ts = (end - start);
                                Console.WriteLine(Messages.SecurityIncorrectLogin + " (" + ts.TotalSeconds + "s)");
                                writer.WriteLine(Messages.SecurityIncorrectLogin + " (" + ts.TotalSeconds + "s)");
                            }
                            else
                            {
                                DateTime end = DateTime.Now;
                                TimeSpan ts = (end - start);
                                Console.WriteLine(Messages.OpenDatabaseSuccess + " (" + ts.TotalSeconds + "s)");
                                writer.WriteLine(Messages.OpenDatabaseSuccess + " (" + ts.TotalSeconds + "s)");
                            }
                        }

                    }
                    else
                    {
                        //catches the line to execute
                        string sentence = FileLines[i];
                        //Captures the time of the RunMiniSQLQuery method execution
                        DateTime start = DateTime.Now;

                        string queryResult = database.RunMiniSqlQuery(sentence, database.GetSecurity().GetUser(username));
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
