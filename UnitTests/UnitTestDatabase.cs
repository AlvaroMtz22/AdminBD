using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buscamineros;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTestDatabase
    {
        List<Table> tables = new List<Table>();
        List<TableColumn> data = new List<TableColumn>();
        Database database;

        [TestMethod]
        public void TestDatabaseConstructor()
        {
            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("aitor", "aitoru", contraseña);
            Assert.AreEqual(database.GetName(), "aitor");
        }

        [TestMethod]
        public void TestAddTable()
        {
            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("aitor", "aitoru", contraseña);
            Table table = new Table("Nombres", data);
            tables.Add(table);
            database.AddTable(table);
            Assert.AreEqual(database.GetList().Count, 1);
        }

        [TestMethod]
        public void TestDeleteTable()
        {
            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("aitor", "aitoru", contraseña);
            //we add table table in order to have data to test if we are able to delete it.
            Table table = new Table("Nombres", data);
            database.AddTable(table);

            Assert.AreEqual(database.GetList().Count, 1);
            database.DeleteTable(table);

            Assert.AreEqual(database.GetList().Count, 0);
        }

        [TestMethod]
        public void TestGetName()
        {
            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("database", "aitoru", contraseña);
            String name = "database";
            Assert.AreEqual(name, database.GetName());
        }

        [TestMethod]
        public void TestUpdate()
        {
            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("aitor", "aitoru", contraseña);
            //Parameters that will be inserted in the function
            List<string> setAttribute = new List<string>();
            setAttribute.Add("Nombre");
            setAttribute.Add("Apellido");
            List<string> value = new List<string>();
            value.Add("Aitor");
            value.Add("Urabain");
            string table = "Empleado";
            CompareWhere compared = new CompareWhere("Nombre","Ronny","=");
            //Creating the elements for changing with the update
            Table tab = new Table("Empleado",new List<TableColumn>());
            TableColumn tablecolumn1 = new TableColumn("Nombre","string");
            tablecolumn1.AddValue("Alvaro");
            tablecolumn1.AddValue("Ronny");
            TableColumn tablecolumn2 = new TableColumn("Apellido", "string");
            tablecolumn2.AddValue("Margo");
            tablecolumn2.AddValue("Caiza");
            tab.AddTableColumn(tablecolumn1);
            tab.AddTableColumn(tablecolumn2);
            database.AddTable(tab);
            // executing the update method
            database.Update(setAttribute, value, table, compared);
            // looking if it has changed
            Boolean welldone = false;
            foreach (TableColumn tc in tab.GetList()) 
            {
                if (tc.GetName() == "Nombre")
                {
                    foreach (string st in tc.GetList())
                    {
                        if (st == "Aitor")
                        {
                            welldone = true;
                        }
                    }
                }
            }
            Assert.AreEqual(true, welldone);
        }

        [TestMethod]
        public void TestSelect()
        {
            System.Security.SecureString password = new System.Security.SecureString();
            Database database1 = new Database("aitor", "aitoru", password);
            //Creating elements for select method parameters
            List<string> selects = new List<string>();
            selects.Add("Name");
            CompareWhere compared = new CompareWhere("Surname", "Caiza", "=");

            Table tab = new Table("Employee", new List<TableColumn>());

            TableColumn tablecolumn1 = new TableColumn("Name", "string");
            tablecolumn1.AddValue("Alvaro");
            tablecolumn1.AddValue("Ronny");

            TableColumn tablecolumn2 = new TableColumn("Surname", "string");
            tablecolumn2.AddValue("Margo");
            tablecolumn2.AddValue("Caiza");

            tab.AddTableColumn(tablecolumn1);
            tab.AddTableColumn(tablecolumn2);
            database1.AddTable(tab);

            Table tableResult = new Table("Result", new List<TableColumn>());
            TableColumn tcResult= new TableColumn("Name", "string");
            tcResult.AddValue("Ronny");
            tableResult.AddTableColumn(tcResult);
            Table nullTable = new Table("nullTable", new List<TableColumn>());
            nullTable.AddTableColumn(tcResult);
            // executing the select method

            Table result = database1.select(tab.GetName(), selects, compared);
            Assert.AreEqual("Ronny", result.GetList().ElementAt(0).GetList().ElementAt(0));
            selects.Add("Surname");
            CompareWhere compared2 = new CompareWhere("Name", "Alvaro", "=");
            Table result2 = database1.select(tab.GetName(), selects, compared2);
            Assert.AreEqual("Alvaro", result2.GetList().ElementAt(0).GetList().ElementAt(0));
            Assert.AreEqual("Margo", result2.GetList().ElementAt(1).GetList().ElementAt(0));
            Assert.AreEqual("Ronny", nullTable.GetList().ElementAt(0).GetList().ElementAt(0));
        }

        [TestMethod]
        public void TestSelectAll()
        {
            //System.Security.SecureString password = new System.Security.SecureString();
            //Database database1 = new Database("aitor", "aitoru", password);

            //Table tab = new Table("Users", new List<TableColumn>());
            //TableColumn tablecolumn1 = new TableColumn("Name", "string");
            //tablecolumn1.AddValue("Alvaro");
            //tablecolumn1.AddValue("Ronny");
            //TableColumn tablecolumn2 = new TableColumn("Age", "int");
            //tablecolumn2.AddValue("20");
            //tablecolumn2.AddValue("22");
            //tab.AddTableColumn(tablecolumn1);
            //tab.AddTableColumn(tablecolumn2);

            database1.AddTable(tab);
            
            CompareWhere compared2 = new CompareWhere("Name", "Alvaro", "=");
            Table newTab = database1.SelectAll(tab.GetName(),compared2);
            Table tab1 = database1.SelectAll(tab.GetName(), null);


            Assert.AreEqual("Users", tab1.GetName());
            Assert.AreEqual(2, tab1.GetList().Count);
            Assert.AreEqual("Alvaro", tab1.GetList().ElementAt(0).GetList().ElementAt(0));
            Assert.AreEqual("Ronny", tab1.GetList().ElementAt(0).GetList().ElementAt(1));
            Assert.AreEqual("20", tab1.GetList().ElementAt(1).GetList().ElementAt(0));
            Assert.AreEqual("22", tab1.GetList().ElementAt(1).GetList().ElementAt(1));

            Assert.AreEqual("Alvaro", newTab.GetList().ElementAt(0).GetList().ElementAt(0));
            Assert.AreEqual(1, newTab.GetList().ElementAt(0).GetList().Count);
            Assert.AreNotEqual("Margo", tab1.GetList().ElementAt(1).GetList().ElementAt(0));


        }

        [TestMethod]
        public void TestDelete()
        {
            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("aitor", "aitoru", contraseña);
            CompareWhere compared = new CompareWhere("Nombre", "Ronny", "=");
            //Creating the elements for changing with the update
            Table tab = new Table("Empleado", new List<TableColumn>());
            TableColumn tablecolumn1 = new TableColumn("Nombre", "string");
            tablecolumn1.AddValue("Alvaro");
            tablecolumn1.AddValue("Ronny");
            TableColumn tablecolumn2 = new TableColumn("Apellido", "string");
            tablecolumn2.AddValue("Margo");
            tablecolumn2.AddValue("Caiza");
            tab.AddTableColumn(tablecolumn1);
            tab.AddTableColumn(tablecolumn2);
            database.AddTable(tab);
            // executing the update method
            database.Delete(tab.GetName(),compared);
            // looking if it has changed
            Boolean welldone = false;
            foreach (TableColumn tc in tab.GetList())
            {
                foreach (string st in tc.GetList())
                {
                    if (st == "Ronny")
                    {
                        welldone = true;
                    }
                }
            }
            Assert.AreNotEqual(true, welldone);
        }

        [TestMethod]
        public void TestInsertInto()
        {
            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("aitor", "aitoru", contraseña);
            //Creating the elements for data in Empleado table

            Table tab = new Table("Empleado", new List<TableColumn>());
            TableColumn tablecolumn1 = new TableColumn("Nombre", "string");
            tablecolumn1.AddValue("Alvaro");
            tablecolumn1.AddValue("Ronny");
            TableColumn tablecolumn2 = new TableColumn("Apellido", "string");
            tablecolumn2.AddValue("Margo");
            tablecolumn2.AddValue("Caiza");
            tab.AddTableColumn(tablecolumn1);
            tab.AddTableColumn(tablecolumn2);
            database.AddTable(tab);
            //Creating the parameters for executing the method
            string table = "Empleado";
            List<string> columns = new List<string>();
            columns.Add("Nombre");
            List<string> values = new List<string>();
            List<string> value2 = new List<string>();
            values.Add("Unai");
            values.Add("Ruiz");
            value2.Add("Aitor");
            // executing the insert method

            database.InsertInto(table,null,values);
            database.InsertInto(table, columns, value2);
            // looking if it has changed
            Boolean welldone = false;
            Boolean welldone2 = false;
            Boolean welldone3 = false;
            foreach (TableColumn tc in tab.GetList())
            {
                foreach (string st in tc.GetList())
                {
                    if (st == "Unai")
                    {
                        welldone = true;
                    }
                    if (st == "Ruiz")
                    {
                        welldone2 = true;
                    }
                    if (st == "Aitor") 
                    {
                        welldone3 = true;
                    }
                }
            }
            if (welldone == true && welldone2 == true&& welldone3==true)
            {
                welldone = true;
            }
            else 
            {
                welldone=false;
            }
            Assert.AreEqual(true, welldone);
        }

        [TestMethod]
        public void TestSaveAndLoad()
        {

            System.Security.SecureString contraseña = new System.Security.SecureString();
            database = new Database("UnitTestDatabase", "UN", contraseña);
            Table tab = new Table("Employee", new List<TableColumn>());
            TableColumn tablecolumn1 = new TableColumn("Name", "string");
            tablecolumn1.AddValue("Alvaro");
            tablecolumn1.AddValue("Ronny");
            TableColumn tablecolumn2 = new TableColumn("Surname", "string");
            tablecolumn2.AddValue("Margo");
            tablecolumn2.AddValue("Caiza");
            tab.AddTableColumn(tablecolumn1);
            tab.AddTableColumn(tablecolumn2);
            database.AddTable(tab);

            database.Save();
            Database db = Database.Load(database.GetName());
            string result = "['Name','Surname']{'Alvaro','Margo'}{'Ronny','Caiza'}";
            Assert.AreEqual(result, db.ToString());

        }
    }
}