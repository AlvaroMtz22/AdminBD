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
        public string CreateSecurityProfile(string profile)
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
                return null;
            }
            else 
            {
                return Messages.SecurityProfileAlreadyExists;
            }
        }
        public string DropSecurityProfile(string profile)
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
            return null;
            
        }
        public string Grant(PrivilegeType privilege ,string table , Profile profile)
        {
            Dictionary<string, List<PrivilegeType>> hashtable= profile.GetHashTable();
            if (hashtable.ContainsKey(table))
            {
                return Messages.SecurityPrivilegeAlreadyExist;
            }
            else
            {
                profile.addPrivilegesInTable(privilege, table);
                return Messages.SecurityPrivilegeGranted;
            }
        }
        public string Revoke(PrivilegeType privilege, string table, Profile profile)
        {
            profile.deletePrivilegesInTable( privilege, table);
            return Messages.SecurityPrivilegeRevoked;
        }

        public string AddUser(string user, string password, Profile profile)
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
            User user = new User(user, password, profile);
            users.Add(user);
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
            foreach (User u in users) 
            {
                
                if (u.getName().equals(user))
                {
                    users.RemoveAt(index);
                }
                index++;
            }
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public List<Profile> GetProfiles()
        {
            return profiles;
        }
    }
    
}
