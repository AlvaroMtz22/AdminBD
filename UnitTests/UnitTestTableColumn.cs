using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            tableColumn.AddValue("Aitor");
            Assert.AreEqual(1, tableColumn.GetList().Count);
        }
        [TestMethod]
        public void TestGetName()
        {
            tableColumn.AddValue("Aitor");
            Assert.AreEqual("Aitor", tableColumn.GetList()[0]);
        }
    }
}
