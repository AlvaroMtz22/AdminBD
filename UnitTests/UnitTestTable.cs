using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buscamineros;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTestTable
    {
        Table table = new Table("Employees", new List<TableColumn>());
        List<TableColumn> list = new List<TableColumn>();

        [TestMethod]
        public void TableConstructorTest()
        {
            Table table2 = new Table("Employees", new List<TableColumn>());
            Assert.IsNotNull(table2);

        }
        [TestMethod]
        public void DeleteTableColumnTest()
        {
            TableColumn tableColumn = new TableColumn("Aitor", "string");
            table.AddTableColumn(tableColumn);
            Assert.AreEqual(1, table.GetList().Count);
            table.DeleteTableColumn(tableColumn);
            Assert.AreEqual(0, table.GetList().Count);

        }
        [TestMethod]
        public void AddTableColumnTest()
        {
            Table t = new Table("Users", new List<TableColumn>());

            Assert.AreEqual(0, t.GetList().Count);

            TableColumn tableColumn = new TableColumn("Aitor", "string");
            t.AddTableColumn(tableColumn);

            Assert.AreEqual(1, t.GetList().Count);

        }

        [TestMethod]
        public void GetNameTest()
        {
            Assert.AreEqual(table.GetName(), "Employees");
        }

        [TestMethod]
        public void AddRowTest()
        {
            List<TableColumn> list1 = new List<TableColumn>();
            TableColumn name = new TableColumn("name", "string");
            TableColumn surname = new TableColumn("surname", "string");
            list1.Add(name);
            list1.Add(surname);
            Table table1 = new Table("Users", list1);

            Assert.AreEqual(0,table1.GetList().ElementAt(0).GetList().Count);
            Assert.AreEqual(0,table1.GetList().ElementAt(1).GetList().Count);

            List<string> values=new List<string>();
            values.Add("Aitor");
            values.Add("Urabain");
            table1.AddRow(values);

            Assert.AreEqual(1, table1.GetList().ElementAt(0).GetList().Count);
            Assert.AreEqual(1, table1.GetList().ElementAt(1).GetList().Count);

        }

        [TestMethod]
        public void GetListTest()
        {
            TableColumn tableColumn = new TableColumn("Aitor", "string");
            table.AddTableColumn(tableColumn);

            list.Add(tableColumn);

            Assert.AreEqual(table.GetList().ElementAt(0), list.ElementAt(0));
        }

        [TestMethod]
        public void GetColumnTest()
        {
            TableColumn tableColumn1 = new TableColumn("column1", "string");
            table.AddTableColumn(tableColumn1);
            TableColumn tableColumn2 = new TableColumn("column2", "string");
            table.AddTableColumn(tableColumn2);
            TableColumn tableColumn3 = new TableColumn("column3", "string");
            table.AddTableColumn(tableColumn3);

            Assert.AreEqual(table.GetColumn("column2"), tableColumn2);
            Assert.AreNotEqual(table.GetColumn("column1"), tableColumn3);

        }
    }
}
