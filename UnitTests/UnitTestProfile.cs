using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buscamineros;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
    [TestClass]
    public class UnitTestProfile
    {
        Table table = new Table("Employees", new List<TableColumn>());
        Table table2 = new Table("Employers", new List<TableColumn>());
        Table table3 = new Table("Others", new List<TableColumn>());
        List<PrivilegeType> privilegeList = new List<PrivilegeType>();
        Profile profile = new Profile("Student");

        [TestMethod]
        public void ProfileConstructorTest()
        {
            Profile p = new Profile("Programmer");
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void AddPrivilegesTest()
        {
            string message = profile.AddPrivilege(PrivilegeType.Select, table.GetName());
            Assert.AreEqual(PrivilegeType.Select, profile.GetHashTable()[table.GetName()].ElementAt(0));
            profile.AddPrivilege(PrivilegeType.Insert, table.GetName());
            profile.AddPrivilege(PrivilegeType.Update, table.GetName());
            profile.AddPrivilege(PrivilegeType.Delete, table2.GetName());
            Assert.AreEqual(PrivilegeType.Delete, profile.GetHashTable()[table2.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Select, profile.GetHashTable()[table.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Insert, profile.GetHashTable()[table.GetName()].ElementAt(1));
            Assert.AreEqual(PrivilegeType.Update, profile.GetHashTable()[table.GetName()].ElementAt(2));
            Assert.AreEqual(Messages.SecurityPrivilegeGranted, message);

            //Error cases
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, profile.AddPrivilege(PrivilegeType.Select, table.GetName()));
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, profile.AddPrivilege(PrivilegeType.Insert, table.GetName()));
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, profile.AddPrivilege(PrivilegeType.Update, table.GetName()));
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, profile.AddPrivilege(PrivilegeType.Delete, table2.GetName()));
        }

        [TestMethod]
        public void DeletePrivilegesTest()
        {
            profile.AddPrivilege(PrivilegeType.Select, table.GetName());
            profile.AddPrivilege(PrivilegeType.Update, table.GetName());
            profile.AddPrivilege(PrivilegeType.Delete, table.GetName());
            Assert.AreEqual(PrivilegeType.Select, profile.GetHashTable()[table.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Update, profile.GetHashTable()[table.GetName()].ElementAt(1));
            Assert.AreEqual(PrivilegeType.Delete, profile.GetHashTable()[table.GetName()].ElementAt(2));

            string message4 = profile.DeletePrivilege(PrivilegeType.Select, table.GetName());
            Assert.AreEqual(PrivilegeType.Update, profile.GetHashTable()[table.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Delete, profile.GetHashTable()[table.GetName()].ElementAt(1));

            //Error cases
            string message = profile.DeletePrivilege(PrivilegeType.Insert, table.GetName());
            Assert.AreEqual(Messages.SecurityPrivilegeDoesNotExist, message);
            string message2 = profile.DeletePrivilege(PrivilegeType.Insert, table3.GetName());
            Assert.AreEqual(Messages.SecurityPrivilegeDoesNotExist, message2);
            string message3 = profile.DeletePrivilege(PrivilegeType.Select, table.GetName());
            Assert.AreEqual(Messages.SecurityPrivilegeDoesNotExist, message3);
            Assert.AreEqual(Messages.SecurityPrivilegeRevoked, message4);

        }
    }
}
