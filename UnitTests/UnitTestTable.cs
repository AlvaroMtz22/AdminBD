using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buscamineros;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTestTable
    {
        Table table = new Table("Employees", new List<TableColumn>());
        
        [TestMethod]
        public void TableConstructorTest()
        {
            Table table2 = new Table("Employees", new List<TableColumn>());
            Assert.AreEqual(table, table2);
            
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

            TableColumn tableColumn=new TableColumn("Aitor","string");
            table.AddTableColumn(tableColumn);
            Assert.AreEqual(1, table.GetList().Count);
        }
        [TestMethod]
        public void getNameTest()
        {
            Assert.AreEqual(table.GetName(), "Employees");
        }
        [TestMethod]
        public void AddRowTest()
        {
            List<string> values=new List<string>();
            values.Add("Aitor");
            values.Add("Urabain");
            table.AddRow(values);
            foreach (TableColumn x in table.GetList()) 
            {
                Assert.AreEqual(1, x.GetList().Count);
            }

        }
    }
}
