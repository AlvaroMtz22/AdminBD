using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class Security
    {
        List<User> users;
        List < Profile > profiles;
        public Security() 
        {
            users= new List<User>();
            profiles = new List<Profile>();
        }
        public void createSecurityProfile(string profile)
        {
            Profile newProfile = new Profile(profile);
            Boolean isAlready = false;
            foreach (Profile p in profiles) 
            {
                if (p.getName() == profile) 
                {
                    isAlready = true;
                }
            }
            if (isAlready == false)
            {
                profiles.Add(newProfile);
            }
            else 
            {
                //mostrar error
            }
        }
        public void dropSecurityProfile(string profile)
        {
            int position = 0;
            foreach (Profile p in profiles)
            {
                if (p.getName() == profile)
                {
                    profiles.RemoveAt(position);
                }
                position++;
            }
            
        }
        public string Grant(PrivilegeType privilege ,Table table , Profile profile)
        {
            Dictionary<string, List<PrivilegeType>> hashtable= profile.GetHashTable();
            if (hashtable.ContainsKey(table.GetName()))
            {
                return Messages.SecurityPrivilegeAlreadyExist;
            }
            else
            {
                profile.addPrivilegesInTable(privilege, table);
                return Messages.SecurityPrivilegeGranted;
            }
        }
        public string Revoke(PrivilegeType privilege, Table table, Profile profile)
        {
            profile.deletePrivilegesInTable( privilege, table);
            return Messages.SecurityPrivilegeRevoked;
        }

        public string AddUser(string user, System.Security.SecureString password, Profile profile)
        {
            bool exists = false;
            foreach (User u in users)
            {

                if (u.getName().equals(user))
                {
                    exists = true;
                }
            }
            if (exists == false) { 
            users.Add(new User(user, password, profile));
                return Messages.SecurityUserCreated;
            }
            else
            {
                return Messages.SecurityUserAlreadyExists;

            }
        }
        public string DeleteUser(string user)
        {
            int index = 1;
            string delete=Messages.SecurityUserDoesNotExist;
            foreach (User u in users) 
            {
                
                if (u.getName().equals(user))
                {
                    users.RemoveAt(index);
                    delete= Messages.SecurityUserDeleted;
                }
                index++;
            }
                return delete;
        }
    }
    
}
