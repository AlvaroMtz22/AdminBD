using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buscamineros;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class UnitTestTableColumn
    {
        TableColumn tableColumn=new TableColumn("age","int");

        [TestMethod]
        public void TableConstructorTest()
        {
            TableColumn tc = new TableColumn("Employees", "string");
            Assert.IsNotNull(tc);

        }

        [TestMethod]
        public void TestAddValue()
        {
            Assert.AreEqual(0, tableColumn.GetList().Count);
            tableColumn.AddValue("Aitor");
            Assert.AreEqual(1, tableColumn.GetList().Count);
        }

        [TestMethod]
        public void TestGetName()
        {
            string name = tableColumn.GetName();
            Assert.AreEqual("age", name);
        }

        [TestMethod]
        public void TestGetColumnType()
        {
            string type = tableColumn.GetColumnType();
            Assert.AreEqual("int", type);
        }

        [TestMethod]
        public void TestGetList()
        {
            tableColumn.AddValue("20");
            List<string> list = tableColumn.GetList();
            Assert.AreEqual("20", list.ElementAt(0));
        }

        [TestMethod]
        public void TestSetList()
        {
            List<string> list = new List<string>();
            list.Add("aaa");
            tableColumn.SetList(list);
            List<string> newList = tableColumn.GetList();
            Assert.AreEqual("aaa", newList.ElementAt(0));
        }

        [TestMethod]
        public void TestGetPositions()
        {
            CompareWhere compare1 = new CompareWhere("age", "20", "=");
            CompareWhere compare2 = new CompareWhere("age", "10", ">");
            CompareWhere compare3 = new CompareWhere("age", "30", "<");

            tableColumn.AddValue("10");
            tableColumn.AddValue("20");
            tableColumn.AddValue("30");

            List<int> positions1 = tableColumn.GetPositions(compare1);
            List<int> positions2 = tableColumn.GetPositions(compare2);
            List<int> positions3 = tableColumn.GetPositions(compare3);

            Assert.AreEqual(1, positions1.Count);
            Assert.AreEqual(1, positions1.ElementAt(0));

            Assert.AreEqual(2, positions2.Count);
            Assert.AreEqual(1, positions2.ElementAt(0));
            Assert.AreEqual(2, positions2.ElementAt(1));

            Assert.AreEqual(2, positions3.Count);
            Assert.AreEqual(0, positions3.ElementAt(0));
            Assert.AreEqual(1, positions3.ElementAt(1));
        }

        [TestMethod]
        public void TestGetValues()
        {
            tableColumn.AddValue("10");
            tableColumn.AddValue("20"); 
            tableColumn.AddValue("30");
            tableColumn.AddValue("40");

            List<int> positions = new List<int>();
            positions.Add(0);
            positions.Add(2);
            positions.Add(3);

            List<string> expectedValues = new List<string>();
            expectedValues.Add("10");
            expectedValues.Add("30");
            expectedValues.Add("40");

            List<string> obtainedValues = tableColumn.GetValues(positions);

            Assert.AreEqual(expectedValues.ElementAt(0), obtainedValues.ElementAt(0));
            Assert.AreEqual(expectedValues.ElementAt(1), obtainedValues.ElementAt(1));
            Assert.AreEqual(expectedValues.ElementAt(2), obtainedValues.ElementAt(2));
        }
    }
}
