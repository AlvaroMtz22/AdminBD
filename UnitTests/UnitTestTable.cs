using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            Assert.AreEqual(1, table.getList().count);
            table.DeleteTableColumn(tableColumn);
            Assert.AreEqual(0, table.getList().count);

        }
        [TestMethod]
        public void AddTableColumnTest()
        {

            TableColumn tableColumn=new TableColumn("Aitor","string");
            table.AddTableColumn(tableColumn);
            Assert.AreEqual(1, table.getList().count);
        }
        [TestMethod]
        public void getNameTest()
        {
            Assert.AreEqual(table.getName(), "Employees");
        }
        [TestMethod]
        public void AddRowTest()
        {
            List<string> values=new List<string>();
            values.Add("Aitor");
            values.Add("Urabain");
            table.AddRow(values);
            foreach (TableColumn x in table.getList()) 
            {
                Assert.AreEqual(1, x.getList().Count());
            }

        }
    }
}
