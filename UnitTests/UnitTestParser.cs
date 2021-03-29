using Buscamineros;
using Buscamineros.MiniSQLParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UnitTestParser
    {
        [TestMethod]
        public void ParseTest()
        {
            //Test select all

            IQuery query = Parser.Parse("SELECT * FROM Ta3ble1;");
            Assert.IsTrue(query is SelectAll);
            Assert.AreEqual("Ta3ble1", (query as SelectAll).Table());

            //Test select all where

            IQuery query2 = Parser.Parse("SELECT * FROM Ta3ble1 WHERE Name='Jon';");
            Assert.IsTrue(query2 is SelectAllWhere);
            Assert.AreEqual("Ta3ble1", (query2 as SelectAllWhere).Table());
            Assert.AreEqual("Name", (query2 as SelectAllWhere).Condition().GetColumn());
            Assert.AreEqual("=", (query2 as SelectAllWhere).Condition().GetComparator());
            Assert.AreEqual("Jon", (query2 as SelectAllWhere).Condition().GetName());

            //Test select columns

            IQuery query3 = Parser.Parse("SELECT Name,Surname,Age FROM Ta3ble1;");
            Assert.IsTrue(query3 is SelectColumns);
            Assert.AreEqual("Ta3ble1", (query3 as SelectColumns).Table());
            Assert.AreEqual("Name", (query3 as SelectColumns).Columns().ElementAt(0));
            Assert.AreEqual("Surname", (query3 as SelectColumns).Columns().ElementAt(1));
            Assert.AreEqual("Age", (query3 as SelectColumns).Columns().ElementAt(2));

            //Test select columns where

            IQuery query4 = Parser.Parse("SELECT Name,Surname,Age FROM Ta3ble1 WHERE Age>'20';");
            Assert.IsTrue(query4 is SelectColumnWhere);
            Assert.AreEqual("Ta3ble1", (query4 as SelectColumnWhere).Table());
            Assert.AreEqual("Name", (query4 as SelectColumnWhere).Columns().ElementAt(0));
            Assert.AreEqual("Surname", (query4 as SelectColumnWhere).Columns().ElementAt(1));
            Assert.AreEqual("Age", (query4 as SelectColumnWhere).Columns().ElementAt(2));
            Assert.AreEqual("Age", (query4 as SelectColumnWhere).Condition().GetColumn());
            Assert.AreEqual(">", (query4 as SelectColumnWhere).Condition().GetComparator());
            Assert.AreEqual("20", (query4 as SelectColumnWhere).Condition().GetName());

            //Test delete

            IQuery query5 = Parser.Parse("DELETE FROM Ta3ble1 WHERE Age<'30';");
            Assert.IsTrue(query5 is Delete);
            Assert.AreEqual("Ta3ble1", (query5 as Delete).Table());
            Assert.AreEqual("Age", (query5 as Delete).Condition().GetColumn());
            Assert.AreEqual("<", (query5 as Delete).Condition().GetComparator());
            Assert.AreEqual("30", (query5 as Delete).Condition().GetName());

            //Test update

            IQuery query6 = Parser.Parse("UPDATE Ta3ble1 SET Name='Pepe',Surname='Garcia',Age='20' WHERE City='Gasteiz';");
            Assert.IsTrue(query6 is Update);
            Assert.AreEqual("Ta3ble1", (query6 as Update).Table());
            Assert.AreEqual("City", (query6 as Update).Condition().GetColumn());
            Assert.AreEqual("=", (query6 as Update).Condition().GetComparator());
            Assert.AreEqual("Gasteiz", (query6 as Update).Condition().GetName());
            Assert.AreEqual("Name", (query6 as Update).SetColumns().ElementAt(0));
            Assert.AreEqual("Surname", (query6 as Update).SetColumns().ElementAt(1));
            Assert.AreEqual("Age", (query6 as Update).SetColumns().ElementAt(2));
            Assert.AreEqual("Pepe", (query6 as Update).SetValues().ElementAt(0));
            Assert.AreEqual("Garcia", (query6 as Update).SetValues().ElementAt(1));
            Assert.AreEqual("20", (query6 as Update).SetValues().ElementAt(2));

            //Test insert into

            IQuery query7 = Parser.Parse("INSERT INTO Ta3ble1 VALUES('Juan','Gonzalez','30');");
            Assert.IsTrue(query7 is InsertInto);
            Assert.AreEqual("Ta3ble1", (query7 as InsertInto).Table());
            Assert.AreEqual("Juan", (query7 as InsertInto).Values().ElementAt(0));
            Assert.AreEqual("Gonzalez", (query7 as InsertInto).Values().ElementAt(1));
            Assert.AreEqual("30", (query7 as InsertInto).Values().ElementAt(2));

            //Test drop table

            IQuery query8 = Parser.Parse("DROP TABLE Ta3ble1;");
            Assert.IsTrue(query8 is DropTable);
            Assert.AreEqual("Ta3ble1", (query8 as DropTable).Table());

            //Test create table

            IQuery query9 = Parser.Parse("CREATE TABLE Ta3ble1(name text, age int, salary double);");
            Assert.IsTrue(query9 is CreateTable);
            Assert.AreEqual("Ta3ble1", (query9 as CreateTable).TableName());
            Assert.AreEqual("Juan", (query9 as CreateTable).Values().ElementAt(0));
        }
    }
}