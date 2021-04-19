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
        public void grant(PrivilegeType privilege ,Table table , Profile profile)
        {
            profile.addPrivilege(table, privilege);
        }
        public void revoke(PrivilegeType privilege, Table table, Profile profile)
        {
            profile.addPrivilege(table, privilege);
        }
       
        public void addUser(string user, string password , Profile profile)
        {
          //  profile.addPrivilege(table, privilege);
        }
        public void deleteUser(string user)
        {
           // profile.addPrivilege(table, privilege);
        }
    }
    
}
