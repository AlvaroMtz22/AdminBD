﻿using Buscamineros.MiniSQLParser;
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
        private System.Security.SecureString m_password;
        private List<Table> m_tables = new List<Table>();

        public Database(string name, string username, System.Security.SecureString
        password)
        {
            m_name = name;
            m_username = username;
            m_password = password;
        }

        public void AddTable(Table table)
        {
            m_tables.Add(table);
        }

        public void DeleteTable(Table table)
        {
            m_tables.Remove(table);
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
            //we have the values where condition is true
            List<int> valuesCompared = t.CompareValues(compared);

            //We create the table which we will return as the select
            values = new Table("selectResult", new List<TableColumn>());

            //we make an iteration for the columns to search those we want
            foreach (TableColumn s in TableColumns)
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

            return values;

        }
        public void InsertInto (string table, List<string> columns, List<string> values)
        {
            Table t = GetTable(table);
            if (columns != null)
            {
                int count = 0;
              
                foreach (string cl in columns)
                {
                    foreach (TableColumn tc in t.GetList()) 
                    {
                        if(tc.GetName() == cl) 
                        {                          
                            foreach (string n in values )
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
                
            }
            else
            {
                t.AddRow(values);
            }
                                                                        
        }

        public void Delete(string table, CompareWhere compared)
        {
            Table t = GetTable(table);

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
        
        public void updateSet(List<string> setAttribute, List<string> value, string table, CompareWhere compared)
        {
            Table t = GetTable(table);
            //we have the values where condition is true
            List<int> valuesCompared = t.CompareValues(compared);
            //we make an iteration for the columns to search those we want
            int count = 0;
            foreach (TableColumn s in t.GetList())
            {
                foreach (string w in setAttribute)
                {

                    //attribute we have to change
                    if (s.GetName().CompareTo(w) == 0)
                    {
                        //loop each position we gonna change
                        foreach (int str in (valuesCompared))
                        {
                            //change value in the position we are
                            s.GetList()[str]=value[count];
                        }
                        count++;
                    }
                    
                }              
            }
        }

        public Table SelectAll(string table)
        {
            return null;
        }

        public string RunMiniSqlQuery(string query)
        {
            IQuery queryObject = MiniSQLParser.Parser.Parse(query);

            return queryObject.Run(this);
        }
        public void Load(string filename)
        {
            string text = File.ReadAllText(filename);

            string[] values = text.Split(new char[] { ',' }); //Separar en función del carácter que consideremos ({ '\n' } / { ',' } / ...)

            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Replace("[[delimiter]]", ","); //reemplazar de vuelta el valor separador por el valor que queremos
            }
        }

        public void Save()
        {

           
            string text = null;
            string tc_val = null;
            string tableColumnText = null;

            if (!Directory.Exists(GetName()))
                Directory.CreateDirectory(GetName());
            string directory = GetName();

            foreach (Table m in m_tables) //recorrer datos que tenemos, en este caso ejemplo para ver
            {
                string tableDirectory = directory + "\\" + m.GetName();
                if (!Directory.Exists(tableDirectory))
                    Directory.CreateDirectory(tableDirectory);
                string tableName = "tableName," + m.GetName();
                text += tableName.Replace(",", "[[delimiter]]") + ","; // reemplazar el valor que queremos (coma) por el valor separador ([[delimiter]])
                
                foreach (TableColumn c in m.GetList()) 
                {
                    string tableColumnDirectory = directory + "\\" + tableDirectory + "\\" + c.GetName();
                    string tableColumnNames = "tableColumnNames," + c.GetName();
                    tableColumnText += tableColumnNames.Replace(",", "[[delimiter]]") + ",";

                    foreach (string val in c.GetList()) 
                    {
                        string tableColumnVal = "tableColumnVal," + val;
                        tc_val += tableColumnVal.Replace(",", "[[delimiter]]") + ",";
                    }
                    File.WriteAllText(tableColumnDirectory, c.GetColumnType() + "[[delimiter]]"+  tc_val);
                }

            }

           
        }
        

    }
}
