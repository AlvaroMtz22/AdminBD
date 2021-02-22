using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buscamineros;
using System.Collections.Generic;

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
            Table table = new Table("Nombres", data);
            tables.Add(table);
            Assert.AreEqual(database.GetList().Count, 1);
            database.AddTable(table);
            Assert.AreEqual(database.GetList(), tables);
        }
        [TestMethod]
        public void TestDeleteTable()
        {
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
            database = new Database("aitor", "aitoru", contraseña);
            String name = "database";
            Assert.AreEqual(name, database.GetName());
        }
    }
}

       
    

