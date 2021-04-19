﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    class Profile
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
        private void addPrivileges(PrivilegeType privilege) 
        {
            
            m_privilege.Add(privilege);

        }
        private void deletePrivileges(PrivilegeType privilege)
        {
            m_privilege.Remove(privilege);
        }

        
        //DataTable dt = new DataTable();
        //tableProfile["User Profiles"] = new List<Privilege>();

        //tableProfile["User Profiles"].add(PrivilegeDelete);
        //tableProfile["User Profiles"].add(PrivilegeInsert);
        //tableProfile["User Profiles"].add(PrivilegeSelect);
        //tableProfile["User Profiles"].add(PrivilegeUpdate);
    }
   
}

