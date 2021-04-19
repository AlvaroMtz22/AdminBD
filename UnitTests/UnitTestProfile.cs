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
        List<PrivilegeType> m_privilege = new List<PrivilegeType>();
        P_Profile profile = new P_Profile("Student", m_privilege);
        PrivilegeType privilege = PrivilegeType.Select;
        Dictionary<string, List<PrivilegeType>> hashtable = new Dictionary<string, List<PrivilegeType>>();

        [TestMethod]
        public void ProfileConstructorTest()
        {
            P_Profile p = new P_Profile("Programmer", m_privilege);
            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void AddPrivilegesInTableTest()
        {
            m_privilege.Add(PrivilegeType.Select);
            hashtable.Add(table.GetName(), m_privilege);

            profile.addPrivilegesInTable(PrivilegeType.Select, table.GetName());

        }

        [TestMethod]
        public void DeletePrivilegesInTableTest()
        {

        }

    }
}
