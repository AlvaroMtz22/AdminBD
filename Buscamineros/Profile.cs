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
        Dictionary<string, List<PrivilegeType>> hashtable;

        public Profile(string name)
        {
            hashtable = new Dictionary<string, List<PrivilegeType>>();
            m_name = name;
        }

        public string AddPrivilege(PrivilegeType privilege, string table)
        {
            if (hashtable.ContainsKey(table))
            {
                if (hashtable[table].Contains(privilege))
                {
                    return Messages.SecurityPrivilegeAlreadyGranted;
                }
                else
                {
                    List<PrivilegeType> newList = hashtable[table];
                    newList.Add(privilege);
                    hashtable[table] = newList;
                    return Messages.SecurityPrivilegeGranted;
                }
            }
            else
            {
                List<PrivilegeType> newList = new List<PrivilegeType>();
                newList.Add(privilege);
                hashtable.Add(table, newList);
                return Messages.SecurityPrivilegeGranted;
            }
        }

        public string DeletePrivilege(PrivilegeType privilege, string table)
        {
            if (hashtable.ContainsKey(table))
            {
                if (hashtable[table].Contains(privilege))
                {
                    List<PrivilegeType> newList = hashtable[table];
                    newList.Remove(privilege);
                    hashtable[table] = newList;
                    return Messages.SecurityPrivilegeRevoked;
                }
                else
                {
                    return Messages.SecurityPrivilegeDoesNotExist;
                }
            }
            else
            {
                return Messages.SecurityPrivilegeDoesNotExist;
            }
        }

        public Dictionary<string, List<PrivilegeType>> GetHashTable()
        {
            return hashtable;
        }

        public string GetName()
        {
            return m_name;
        }

    }
   
}

