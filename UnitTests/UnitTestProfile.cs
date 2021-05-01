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
        static List<PrivilegeType> m_privilege = new List<PrivilegeType>();
        Profile profile = new Profile("Student");
        PrivilegeType privilege = PrivilegeType.Select;
        Dictionary<string, List<PrivilegeType>> hashtable = new Dictionary<string, List<PrivilegeType>>();

        [TestMethod]
        public void ProfileConstructorTest()
        {
            Profile p = new Profile("Programmer");
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void AddPrivilegesTest()
        {
            profile.AddPrivilege(PrivilegeType.Select, table.GetName());
            Assert.AreEqual(profile.GetHashTable()[table.GetName()],PrivilegeType.Select);
        }

        [TestMethod]
        public void DeletePrivilegesTest()
        {
            profile.DeletePrivilege(PrivilegeType.Select, table.GetName());
            Assert.AreEqual(profile.GetHashTable()[table.GetName()], null);
        }

    }
}
