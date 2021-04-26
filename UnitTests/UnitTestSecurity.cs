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
        [TestMethod]
        public void TestSecurityConstructor()
        {
            Assert.IsNotNull(sec);
        }
        [TestMethod]
        public void TestCreateSecurityProfile()
        {
            Assert.AreEqual(0, sec.GetProfiles().Count());
            
            string success = sec.CreateSecurityProfile("Employee");
            Assert.AreEqual(1,sec.GetProfiles().Count());
            Assert.AreEqual("Employee", sec.GetProfiles().ElementAt(0));
            Assert.AreEqual(Messages.SecurityProfileCreated, success);

            //Trying to create an already existing profile
            string error = sec.CreateSecurityProfile("Employee");
            Assert.AreEqual(Messages.SecurityProfileAlreadyExists, error);
        }

        [TestMethod]
        public void TestDropSecurityProfile()
        {
            Assert.AreEqual(0, sec.GetProfiles().Count());

            sec.CreateSecurityProfile("Employee");
            Assert.AreEqual(1, sec.GetProfiles().Count());

            string success = sec.DropSecurityProfile("Employee");
            Assert.AreEqual(0, sec.GetProfiles().Count());
            Assert.AreEqual(Messages.SecurityProfileDeleted, success);

            //Trying to delete a non existing profile
            string error = sec.DropSecurityProfile("Employee");
            Assert.AreEqual(Messages.SecurityProfileDoesNotExist, error);
        }

        [TestMethod]
        public void TestGrantPrivilege()
        {
            //non existing table
            string error1 = sec.Grant(PrivilegeType.Delete, "table", "Employee");
            Assert.AreEqual(Messages.TableDoesNotExist, error1);

            //non existing profile
            sec.Grant(PrivilegeType.Delete, "table", "Employee");
            Assert.AreEqual(Messages.TableDoesNotExist, error1);

            sec.GetProfile("Employee").GetHashTable().

        }

        [TestMethod]
        public void TestRevokePrivilege()
        {

        }

        [TestMethod]
        public void TestAddUser()
        {
            Assert.AreEqual(0, sec.GetUsers().Count());

            Profile pr = new Profile("Employee", new List<PrivilegeType>());
            string success = sec.AddUser("UserName","MyPassword",pr);

            Assert.AreEqual(1, sec.GetUsers().Count());
            Assert.AreEqual(Messages.SecurityUserAdded, success);

            //Trying to add an already existing user
            string error = sec.AddUser("UserName", "MyPassword", pr);
            Assert.AreEqual(Messages.SecurityUserAlreadyExists, error);
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Profile pr = new Profile("Employee", new List<PrivilegeType>());
            sec.AddUser("UserName", "MyPassword", pr);

            Assert.AreEqual(1, sec.GetUsers().Count());

            string success = sec.DeleteUser("UserName");
            Assert.AreEqual(Messages.SecurityUserDeleted, success);

            //Trying to delete a non existing user
            string error = sec.DeleteUser("UserName");
            Assert.AreEqual(Messages.SecurityUserDoesNotExist, error);
        }
    }
}
