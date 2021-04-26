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
        Profile profile = new Profile("Student", m_privilege);
        PrivilegeType privilege = PrivilegeType.Select;
        Dictionary<string, List<PrivilegeType>> hashtable = new Dictionary<string, List<PrivilegeType>>();

        [TestMethod]
        public void ProfileConstructorTest()
        {
            Profile p = new Profile("Programmer", m_privilege);
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void AddPrivilegesInTableTest()
        {
            profile.addPrivilegesInTable(PrivilegeType.Select, table);
            Assert.AreEqual(profile.GetHashTable().Values[0],PrivilegeType.Select);

        }

        [TestMethod]
        public void DeletePrivilegesInTableTest()
        {

        }

    }
}
