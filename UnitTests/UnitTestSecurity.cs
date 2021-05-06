using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buscamineros;

namespace UnitTests
{
    [TestClass]
    public class UnitTestSecurity
    {
        Security sec = new Security();
        User admin = new User("admin", "admin", new Profile("admin"));
        User notAdmin = new User("notAdmin", "notAdmin", new Profile("notAdmin"));
        [TestMethod]
        public void TestSecurityConstructor()
        {
            Assert.IsNotNull(sec);
        }
        [TestMethod]
        public void TestCreateSecurityProfile()
        {
            Assert.AreEqual(0, sec.GetProfiles().Count());

            string success = sec.CreateSecurityProfile("Employee", admin);
            Assert.AreEqual(1, sec.GetProfiles().Count());
            Assert.AreEqual("Employee", sec.GetProfiles().ElementAt(0).GetName());
            Assert.AreEqual(Messages.SecurityProfileCreated, success);

            //Trying to create an already existing profile
            string error = sec.CreateSecurityProfile("Employee", admin);
            Assert.AreEqual(Messages.SecurityProfileAlreadyExists, error);

            //not admin user
            string error2 = sec.CreateSecurityProfile("Employer", notAdmin);
            Assert.AreEqual(Messages.SecurityNotSufficientPrivileges, error2);

        }

        [TestMethod]
        public void TestDropSecurityProfile()
        {
            Assert.AreEqual(0, sec.GetProfiles().Count());

            sec.CreateSecurityProfile("Employee", admin);
            Assert.AreEqual(1, sec.GetProfiles().Count());

            string success = sec.DropSecurityProfile("Employee", admin);
            Assert.AreEqual(0, sec.GetProfiles().Count());
            Assert.AreEqual(Messages.SecurityProfileDeleted, success);

            //Trying to delete a non existing profile
            string error = sec.DropSecurityProfile("Employee", admin);
            Assert.AreEqual(Messages.SecurityProfileDoesNotExist, error);

            //not admin user
            string error2 = sec.DropSecurityProfile("Employee", notAdmin);
            Assert.AreEqual(Messages.SecurityNotSufficientPrivileges, error2);
        }

        [TestMethod]
        public void TestGrantPrivilege()
        {

            //non existing profile
            string error1 = sec.GrantPrivilege(PrivilegeType.Delete, "table", "Employee", admin);
            Assert.AreEqual(Messages.SecurityProfileDoesNotExist, error1);

            sec.CreateSecurityProfile("Employee", admin);
            Profile pr = sec.GetProfile("Employee");
            Table table = new Table("Employees", new List<TableColumn>());
            Table table2 = new Table("Employers", new List<TableColumn>());

            string message = sec.GrantPrivilege(PrivilegeType.Select, table.GetName(), "Employee", admin);
            Assert.AreEqual(PrivilegeType.Select, pr.GetHashTable()[table.GetName()].ElementAt(0));
            sec.GrantPrivilege(PrivilegeType.Insert, table.GetName(), "Employee", admin);
            sec.GrantPrivilege(PrivilegeType.Update, table.GetName(), "Employee", admin);
            sec.GrantPrivilege(PrivilegeType.Delete, table2.GetName(), "Employee", admin);
            Assert.AreEqual(PrivilegeType.Delete, pr.GetHashTable()[table2.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Select, pr.GetHashTable()[table.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Insert, pr.GetHashTable()[table.GetName()].ElementAt(1));
            Assert.AreEqual(PrivilegeType.Update, pr.GetHashTable()[table.GetName()].ElementAt(2));
            Assert.AreEqual(Messages.SecurityPrivilegeGranted, message);

            //Error cases
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, sec.GrantPrivilege(PrivilegeType.Select, table.GetName(), "Employee", admin));
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, sec.GrantPrivilege(PrivilegeType.Insert, table.GetName(), "Employee", admin));
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, sec.GrantPrivilege(PrivilegeType.Update, table.GetName(), "Employee", admin));
            Assert.AreEqual(Messages.SecurityPrivilegeAlreadyGranted, sec.GrantPrivilege(PrivilegeType.Delete, table2.GetName(), "Employee", admin));

            //not admin user
            string error = sec.GrantPrivilege(PrivilegeType.Delete, "table", "Employee", notAdmin);
            Assert.AreEqual(Messages.SecurityNotSufficientPrivileges, error);
        }

        [TestMethod]
        public void TestRevokePrivilege()
        {
            //non existing profile
            string error1 = sec.RevokePrivilege(PrivilegeType.Delete, "table", "Employee", admin);
            Assert.AreEqual(Messages.SecurityProfileDoesNotExist, error1);

            sec.CreateSecurityProfile("Employee", admin);
            Profile pr = sec.GetProfile("Employee");
            Table table = new Table("Employees", new List<TableColumn>());
            Table table2 = new Table("Employers", new List<TableColumn>());
            Table table3 = new Table("Others", new List<TableColumn>());

            sec.GrantPrivilege(PrivilegeType.Insert, table.GetName(), "Employee", admin);
            sec.GrantPrivilege(PrivilegeType.Update, table.GetName(), "Employee", admin);
            sec.GrantPrivilege(PrivilegeType.Delete, table.GetName(), "Employee", admin);
            Assert.AreEqual(PrivilegeType.Insert, pr.GetHashTable()[table.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Update, pr.GetHashTable()[table.GetName()].ElementAt(1));
            Assert.AreEqual(PrivilegeType.Delete, pr.GetHashTable()[table.GetName()].ElementAt(2));

            string message4 = sec.RevokePrivilege(PrivilegeType.Insert, table.GetName(), "Employee", admin);
            Assert.AreEqual(PrivilegeType.Update, pr.GetHashTable()[table.GetName()].ElementAt(0));
            Assert.AreEqual(PrivilegeType.Delete, pr.GetHashTable()[table.GetName()].ElementAt(1));

            //Error cases
            string message = sec.RevokePrivilege(PrivilegeType.Insert, table.GetName(), "Employee", admin);
            Assert.AreEqual(Messages.SecurityPrivilegeDoesNotExist, message);
            string message2 = sec.RevokePrivilege(PrivilegeType.Insert, table3.GetName(), "Employee", admin);
            Assert.AreEqual(Messages.SecurityPrivilegeDoesNotExist, message2);
            string message3 = sec.RevokePrivilege(PrivilegeType.Select, table.GetName(), "Employee", admin);
            Assert.AreEqual(Messages.SecurityPrivilegeDoesNotExist, message3);
            Assert.AreEqual(Messages.SecurityPrivilegeRevoked, message4);

            //not admin user
            string error = sec.RevokePrivilege(PrivilegeType.Delete, "table", "Employee", notAdmin);
            Assert.AreEqual(Messages.SecurityNotSufficientPrivileges, error);
        }

        [TestMethod]
        public void TestAddUser()
        {
            Assert.AreEqual(0, sec.GetUsers().Count());

            Profile pr = new Profile("Employee");
            string success = sec.AddUser("UserName", "MyPassword", pr.GetName(), admin);

            Assert.AreEqual(1, sec.GetUsers().Count());
            Assert.AreEqual(Messages.SecurityUserAdded, success);

            //Trying to add an already existing user
            string error = sec.AddUser("UserName", "MyPassword", pr.GetName(), admin);
            Assert.AreEqual(Messages.SecurityUserAlreadyExists, error);

            //not admin user
            string error2 = sec.AddUser("UserName2", "MyPassword2", pr.GetName(), notAdmin);
            Assert.AreEqual(Messages.SecurityNotSufficientPrivileges, error2);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Profile pr = new Profile("Employee");
            sec.AddUser("UserName", "MyPassword", pr.GetName(), admin);

            Assert.AreEqual(1, sec.GetUsers().Count());

            string success = sec.DeleteUser("UserName", admin);
            Assert.AreEqual(Messages.SecurityUserDeleted, success);

            //Trying to delete a non existing user
            string error = sec.DeleteUser("UserName", admin);
            Assert.AreEqual(Messages.SecurityUserDoesNotExist, error);

            //not admin user
            string error2 = sec.DeleteUser("UserName2", notAdmin);
            Assert.AreEqual(Messages.SecurityNotSufficientPrivileges, error2);
        }
    }
}
