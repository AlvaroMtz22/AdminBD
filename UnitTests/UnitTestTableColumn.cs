using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Buscamineros;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTestTableColumn
    {
        TableColumn tableColumn=new TableColumn("name","string");
        [TestMethod]
        public void TestAddValue()
        {
            tableColumn.addValue("Aitor");
            Assert.AreEqual(1, tableColumn.getList().Count());
        }
        [TestMethod]
        public void TestGetName()
        {
            tableColumn.addValue("Aitor");
            Assert.AreEqual("Aitor", tableColumn.getList()[0]);
        }
    }
}
