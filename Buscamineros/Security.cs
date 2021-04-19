using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class Security
    {
        List<User> users= new List<User>();
        List<Profile> profiles = new List<Profile>();
        public void CreateSecurityProfile(string profile)
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
        public void DropSecurityProfile(string profile)
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
            profile.addPrivilege(table, privilege);
            return Messages.SecurityPrivilegeGranted;
        }
        public string Revoke(PrivilegeType privilege, Table table, Profile profile)
        {
            profile.deletePrivilege(table, privilege);
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
    }
    
}
