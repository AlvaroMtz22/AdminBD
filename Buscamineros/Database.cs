using Buscamineros.MiniSQLParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Buscamineros
{
    public class Database
    {
        private string m_name;
        private string m_username;
        private string m_password;
        private List<Table> m_tables = new List<Table>();
        private Security m_security;

        public Database(string name, string username, string password)
        {
            m_name = name;
            m_username = username;
            m_password = password;
            m_security = new Security();
        }

        public void AddTable(Table table)
        {
            m_tables.Add(table);
        }

        public string CreateTable(string tableName, List<TableColumn> columns)
        { 
            Table table = new Table(tableName, columns);
            AddTable(table);
            return Messages.CreateTableSuccess;
        }

        public string DeleteTable(string table)
        {
            if (m_tables.Contains(GetTable(table)))
            {
                m_tables.Remove(GetTable(table));
                return Messages.DeleteTableSuccess;
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
            
        }
        public Table GetTable(string table)
        {
            foreach (Table t in m_tables)
            {
                if (table == t.GetName())
                {
                    return t;
                }
            }
            return null;
        }

        public string GetName()
        {
            return m_name;
        }

        public List<Table> GetList()
        {
            return m_tables;
        }

        public Table select(string table, List<string> selects, CompareWhere compared)
        {
            Table values = null;
            List<TableColumn> select = new List<TableColumn>();
            Table t = GetTable(table);
            List<TableColumn> TableColumns = t.GetList();
            TableColumn column;

            //we get the select list of columns
            foreach (string s in selects)
            {
                select.Add(t.GetColumn(s));
            }

            //We create the table which we will return as the select
            values = new Table("selectResult", new List<TableColumn>());

            //We see if we have any condition
            if (compared == null)
            {
                //we make an iteration for the columns
                foreach (TableColumn s in select)
                {
                    //we create a column that we will add in the return list
                    column = new TableColumn(s.GetName(), s.GetColumnType());

                    //we get each value we want
                    foreach (string value in s.GetList())
                    {
                        column.AddValue(value);
                    }
                    values.AddTableColumn(column);
                }
            }
            else
            {
                //we have the values where condition is true
                List<int> valuesCompared = t.CompareValues(compared);

                //we make an iteration for the columns to search those we want
                foreach (TableColumn s in select)
                {

                    //we create a column that we will add in the return list
                    column = new TableColumn(s.GetName(), s.GetColumnType());

                    //we get each value we want
                    foreach (string value in s.GetValues(valuesCompared))
                    {
                        column.AddValue(value);
                    }
                    values.AddTableColumn(column);
                }
            }
            return values;

        }

        public Table SelectAll(string table, CompareWhere compared)
        {
            Table values = null;
            List<TableColumn> select = new List<TableColumn>();
            Table t = GetTable(table);
            List<TableColumn> TableColumns = t.GetList();
            TableColumn column;

            

            //We create the table which we will return as the select
            values = new Table("selectResult", new List<TableColumn>());

            //We see if we have any condition
            if (compared == null)
            {
                values = GetTable(table);
            }
            else
            {
                //we have the values where condition is true
                List<int> valuesCompared = t.CompareValues(compared);

                //we make an iteration for the columns to search those we want
                foreach (TableColumn s in t.GetList())
                {

                    //we create a column that we will add in the return list
                    column = new TableColumn(s.GetName(), s.GetColumnType());

                    //we get each value we want
                    foreach (string value in s.GetValues(valuesCompared))
                    {
                        column.AddValue(value);
                    }
                    values.AddTableColumn(column);
                }
            }
            return values;

        }
    

        public string InsertInto(string table, List<string> columns, List<string> values)
        {
            Table t = GetTable(table);
            if (m_tables.Contains(t))
            {
                if (columns != null)
                {
                    int count = 0;

                    foreach (string cl in columns)
                    {
                        if (t.GetList().Contains(t.GetColumn(cl)))
                        {

                            foreach (TableColumn tc in t.GetList())
                            {
                                if (tc.GetName() == cl)
                                {
                                    foreach (string n in values)
                                    {
                                        tc.AddValue(n);
                                    }
                                    count++;
                                }
                                else
                                {
                                    tc.AddValue("null");
                                }
                            }
                        }
                        else
                        {
                            return Messages.ColumnDoesNotExist;
                        }
                    }
                }
                else
                {
                    t.AddRow(values);
                }
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
            return Messages.InsertSuccess;
        }

        public string Delete(string table, CompareWhere compared)
        {
            Table t = GetTable(table);
            if (m_tables.Contains(t))
            {
                if (t.GetList().Contains(t.GetColumn(compared.GetColumn())))
                {
                    List<int> positions = t.CompareValues(compared);
                    foreach (TableColumn tc in t.GetList())
                    {
                        List<string> newList = new List<string>();
                        for (int i = 0; i < tc.GetList().Count; i++)
                        {
                            if (!positions.Contains(i))
                            {
                                newList.Add(tc.GetList().ElementAt(i));
                            }
                        }
                        tc.SetList(newList);
                    }
                }
                else
                {
                    return Messages.ColumnDoesNotExist;
                }
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
            return Messages.TupleDeleteSuccess;
        }

        public string Update(List<string> setAttribute, List<string> value, string table, CompareWhere compared)
        {
            Table t = GetTable(table);
            if (m_tables.Contains(t))
            {
                if (t.GetList().Contains(t.GetColumn(compared.GetColumn())))
                {
                    //we have the values where condition is true
                    List<int> valuesCompared = t.CompareValues(compared);
                    //we make an iteration for the columns to search those we want
                    int count = 0;
                    foreach (TableColumn s in t.GetList())
                    {
                        foreach (string cl in setAttribute)
                        {
                            if (t.GetList().Contains(t.GetColumn(cl)))
                            {
                                //attribute we have to change
                                if (s.GetName().CompareTo(cl) == 0)
                                {
                                    //loop each position we gonna change
                                    foreach (int str in (valuesCompared))
                                    {
                                        //change value in the position we are
                                        s.GetList()[str] = value[count];
                                    }
                                    count++;
                                }

                            }
                            else
                            {
                                return Messages.ColumnDoesNotExist;
                            }
                        }
                    }
                }
                else
                {
                    return Messages.ColumnDoesNotExist;
                }
            }
            else
            {
                return Messages.TableDoesNotExist;
            }
            return Messages.TupleUpdateSuccess;
        }
        public override string ToString() 
        {
            string result = "";
            foreach (Table table in m_tables)
            {
                result+=table.ToString();
            }
            return result;
        } 

        public string RunMiniSqlQuery(string query)
        {
            IQuery queryObject = MiniSQLParser.Parser.Parse(query);
            if(queryObject != null)
            {
                return queryObject.Run(this);
            }
            else
            {
                return Messages.WrongSyntax;
            }
            
        }
        public static Database Load(string dbName)
        {        
            string password = "1234";
            Database db = new Database(dbName, "RonnyAitor", password);
            Table table;
            TableColumn tableColumn;
            foreach (string folder in Directory.GetDirectories(dbName))
            {
                string[] folders = folder.Split(new char[] { '\\' });
                table= new Table(folders[1], new List<TableColumn>());
                string[] filesF=Directory.GetFiles(folder);
                foreach (string file in filesF)
                {
                    string text = File.ReadAllText(file);
                    string[] values = text.Split(new char[] { ',' });
                    string data="";
                    string[] finalData;
                    for (int i = 0; i < values.Length - 1; i++)
                    {
                        values[i] = values[i].Replace("[[delimiter]]", ",");
                        data += values[i] + ",";
                    }//All is on Commas
                    finalData = data.Split(new char[] { ',' });
                    //Separated array
                    //Create the tableColumn
                    string[] files = file.Split(new char[] { '\\' });
                    tableColumn = new TableColumn(files[2],finalData[0]);
                    for (int i = 0; i < finalData.Length; i++)
                    {
                        if (i != 0  && i % 2 == 0) 
                        {
                            tableColumn.AddValue(finalData[i]);
                        }
                    }
                    table.AddTableColumn(tableColumn);            
                }
                db.AddTable(table);
            }
            return db;
        }

        public void Save()
        {
            string text = null;
            string tc_val = null;
            string tableColumnText = null;

            if (!Directory.Exists(GetName()))
                Directory.CreateDirectory(GetName());
            string directory = this.GetName();       //Obtain the name of the Database

            foreach (Table m in m_tables) 
            {
                string tableDirectory = directory + "\\" + m.GetName(); //Obtain the name of an especific table
                if(!Directory.Exists(tableDirectory))
                    Directory.CreateDirectory(tableDirectory);
                string tableName = "tableName," + m.GetName();
                text += tableName.Replace(",", "[[delimiter]]") + ","; 
                
                foreach (TableColumn c in m.GetList()) 
                {
                    string tableColumnDirectory = tableDirectory + "\\" + c.GetName();
                    string tableColumnNames = "tableColumnNames," + c.GetName();
                    tableColumnText += tableColumnNames.Replace(",", "[[delimiter]]") + ",";

                    foreach (string val in c.GetList())  
                    {                                    
                        string tableColumnVal = "tableColumnVal," + val;
                        tc_val += tableColumnVal.Replace(",", "[[delimiter]]") + ",";
                    }
                    File.WriteAllText(tableColumnDirectory, c.GetColumnType() + "[[delimiter]]"+  tc_val);
                    tc_val = "";
                }     
            }   
        }
        public Security GetSecurity()
        {
            return m_security;
        }
    }
}