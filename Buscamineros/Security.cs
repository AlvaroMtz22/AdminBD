using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    public class Security
    {
        List <User> users;
        List <Profile> profiles;

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
                if (p.GetName() == profile) 
                {
                    isAlready = true;
                }
            }
            if (isAlready == false)
            {
                profiles.Add(newProfile);
                return Messages.SecurityProfileCreated;
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
                if (p.GetName() == profile)
                {
                    profiles.RemoveAt(position);
                    return Messages.SecurityProfileDeleted;
                }
                position++;
            }

            return Messages.SecurityProfileDoesNotExist;    
        }

        public string GrantPrivilege(PrivilegeType privilege, string table, string profile)
        {
            Profile profil = GetProfile(profile);
            if (profil.Equals(null))
            {
                return Messages.SecurityProfileDoesNotExist;
            }
            else
            {
                return profil.AddPrivilege(privilege, table);
            }

        }

        public string RevokePrivilege(PrivilegeType privilege, string table, string profile)
        {
            
            Profile profil = GetProfile(profile);
            if (profil.Equals(null))
            {
                return Messages.SecurityProfileDoesNotExist;
            }
            else
            {
                return profil.DeletePrivilege(privilege, table);
            }
        }

        public string AddUser(string user, string password, string profile)
        {
            bool exists = false;
            foreach (User u in users)
            {
                if (u.GetName() == user)
                {
                    exists = true;
                }
            }

            if (exists == false) 
            { 
                users.Add(new User(user, password, GetProfile(profile)));
                return Messages.SecurityUserCreated;
            }

            else
            {
                return Messages.SecurityUserAlreadyExists;
            }
        }

        public string DeleteUser(string user)
        {
            int index = 0;
            foreach (User u in users) 
            {
                if (u.GetName() == user)
                {
                    users.RemoveAt(index);
                    return Messages.SecurityUserDeleted;
                }
                index++;
            }

            return Messages.SecurityUserDoesNotExist;
        }

        public Profile GetProfile(string profile)
        {
            foreach(Profile p in profiles)
            {
                if(p.GetName() == profile)
                {
                    return p;
                }
            }

            return null; 
        }
    }
    
}
