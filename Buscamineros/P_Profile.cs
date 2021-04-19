using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
     public class Profile
    {
        private string m_name;
        List<PrivilegeType> m_privilege = new List<PrivilegeType>();
        Dictionary<string, List<PrivilegeType>> hashtable;
        public Profile(string name, List<PrivilegeType> privilege)
        {
            hashtable = new Dictionary<string, List<PrivilegeType>>();
            m_name = name;
            m_privilege = privilege;
        }
        public void addPrivileges(PrivilegeType privilege)
        {
            foreach (PrivilegeType p in privilege)
            {
                if (p.g == profile)
                {
                    profiles.RemoveAt(position);
                }
                m_privilege.Add(privilege);

        }
        public void deletePrivileges(PrivilegeType privilege)
        {
            m_privilege.Remove(privilege);
        }
        public void addPrivilegesInTable(PrivilegeType privilege, Table table)
        {

            m_privilege.Add(privilege);

        }
        public void deletePrivilegesInTable(PrivilegeType privilege, Table table)
        {
            m_privilege.Remove(privilege);
        }
        public List<string> GetKeyValues()
        {
            return null;
        }
        public Dictionary<string, List<PrivilegeType>> GetHashTable()
        {
            return null;
        }

        public List<PrivilegeType> GetValues() 
        {
            return null;
        }

        //DataTable dt = new DataTable();
        //tableProfile["User Profiles"] = new List<Privilege>();

        //tableProfile["User Profiles"].add(PrivilegeDelete);
        //tableProfile["User Profiles"].add(PrivilegeInsert);
        //tableProfile["User Profiles"].add(PrivilegeSelect);
        //tableProfile["User Profiles"].add(PrivilegeUpdate);

    }
   
}

