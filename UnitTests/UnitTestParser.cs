using Buscamineros;
using Buscamineros.MiniSQLParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

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

            IQuery query7 = Parser.Parse("INSERT INTO Ta3ble1 VALUES ('Juan','Gonzalez',30,'Unai',54334.453);");
            Assert.IsTrue(query7 is InsertInto);
            Assert.AreEqual("Ta3ble1", (query7 as InsertInto).Table());
            Assert.AreEqual("Juan", (query7 as InsertInto).Values().ElementAt(0));
            Assert.AreEqual("Gonzalez", (query7 as InsertInto).Values().ElementAt(1));
            Assert.AreEqual("30", (query7 as InsertInto).Values().ElementAt(2));
            Assert.AreEqual("Unai", (query7 as InsertInto).Values().ElementAt(3));
            Assert.AreEqual("54334.453", (query7 as InsertInto).Values().ElementAt(4));

            //Test drop table

            IQuery query8 = Parser.Parse("DROP TABLE Ta3ble1;");
            Assert.IsTrue(query8 is DropTable);
            Assert.AreEqual("Ta3ble1", (query8 as DropTable).Table());

            //Test create table

            IQuery query9 = Parser.Parse("CREATE TABLE Ta3ble1 (name TEXT,age INT,salary DOUBLE);");
            Assert.IsTrue(query9 is CreateTable);
            Assert.AreEqual("Ta3ble1", (query9 as CreateTable).TableName());
            Assert.AreEqual("name", (query9 as CreateTable).Columns().ElementAt(0).GetName());
            Assert.AreEqual("TEXT", (query9 as CreateTable).Columns().ElementAt(0).GetColumnType());
            Assert.AreEqual("age", (query9 as CreateTable).Columns().ElementAt(1).GetName());
            Assert.AreEqual("INT", (query9 as CreateTable).Columns().ElementAt(1).GetColumnType());
            Assert.AreEqual("salary", (query9 as CreateTable).Columns().ElementAt(2).GetName());
            Assert.AreEqual("DOUBLE", (query9 as CreateTable).Columns().ElementAt(2).GetColumnType());

            //Test grant privilege

            IQuery query13 = Parser.Parse("GRANT INSERT ON Ta3ble1 TO Employee;");
            Assert.IsTrue(query13 is GrantPrivilege);
            Assert.AreEqual("Ta3ble1", (query13 as GrantPrivilege).Table());
            Assert.AreEqual(PrivilegeType.Insert, (query13 as GrantPrivilege).Privilege());
            Assert.AreEqual("Employee", (query13 as GrantPrivilege).Profile());

            //Test revoke privilege

            IQuery query14 = Parser.Parse("REVOKE INSERT ON Ta3ble1 TO Employee;");
            Assert.IsTrue(query14 is RevokePrivilege);
            Assert.AreEqual("Ta3ble1", (query14 as RevokePrivilege).Table());
            Assert.AreEqual(PrivilegeType.Insert, (query14 as RevokePrivilege).Privilege());
            Assert.AreEqual("Employee", (query14 as RevokePrivilege).Profile());

            //Test add user

            IQuery query15 = Parser.Parse("ADD USER ('UserName','MyPassword',Employee);");
        

            Assert.IsTrue(query15 is AddUser);
            Assert.AreEqual("UserName", (query15 as AddUser).User());
            Assert.AreEqual("MyPassword", (query15 as AddUser).Password());
            Assert.AreEqual("Employee", (query15 as AddUser).Profile());

            //Test create security profile

            IQuery query10 = Parser.Parse("CREATE SECURITY PROFILE Employee;");
            Assert.IsTrue(query10 is CreateSecurityProfile);
            Assert.AreEqual("Employee", (query10 as CreateSecurityProfile).Profile());

            //Test delete security profile

            IQuery query11 = Parser.Parse("DROP SECURITY PROFILE Employee;");
            Assert.IsTrue(query11 is DeleteSecurityProfile);
            Assert.AreEqual("Employee", (query11 as DeleteSecurityProfile).Profile());

            //Test delete security profile

            IQuery query12 = Parser.Parse("DELETE USER user;");
            Assert.IsTrue(query12 is DeleteUser);
            Assert.AreEqual("user", (query12 as DeleteUser).User());
        }
    }
}