using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Buscamineros.MiniSQLParser;
using System.IO;

namespace Buscamineros
{
    public class Security
    {
        List <User> users;
        List <Profile> profiles;

        public Security() 
        {
            users= new List<User>();
            users.Add(new User("admin", "admin", new Profile("admin")));
            profiles = new List<Profile>();
        }

        public string CreateSecurityProfile(string profile, User user)
        {
            if (!(user.GetName() == "admin"))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            else
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
        }

        public string DropSecurityProfile(string profile, User user)
        {
            if (!(user.GetName() == "admin"))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            else
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
        }

        public string GrantPrivilege(PrivilegeType privilege, string table, string profile, User user)
        {
            if (!(user.GetName() == "admin"))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            else
            {
                Profile pr = GetProfile(profile);

                if (profiles.Contains(pr))
                {
                    return pr.AddPrivilege(privilege, table);
                }
                else
                {
                    return Messages.SecurityProfileDoesNotExist;
                }
            }
        }

        public string RevokePrivilege(PrivilegeType privilege, string table, string profile, User user)
        {
            if (!(user.GetName() == "admin"))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            else
            {
                Profile pr = GetProfile(profile);
                if (!profiles.Contains(pr))
                {
                    return Messages.SecurityProfileDoesNotExist;
                }
                else
                {
                    return pr.DeletePrivilege(privilege, table);
                }
            }
        }

        public string AddUser(string userToAdd, string password, string profile, User user)
        {
            if (!(user.GetName() == "admin"))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            else
            {
                bool exists = false;
                foreach (User u in users)
                {
                    if (u.GetName() == userToAdd)
                    {
                        exists = true;
                    }
                }

                if (exists == false)
                {
                    users.Add(new User(userToAdd, password, GetProfile(profile)));
                    return Messages.SecurityUserAdded;
                }
                else
                {
                    return Messages.SecurityUserAlreadyExists;
                }
            }
        }

        public string DeleteUser(string userToDelete, User user)
        {
            if (!(user.GetName() == "admin"))
            {
                return Messages.SecurityNotSufficientPrivileges;
            }
            else
            {
                int index = 0;
                foreach (User u in users)
                {
                    if (u.GetName() == userToDelete)
                    {
                        users.RemoveAt(index);
                        return Messages.SecurityUserDeleted;
                    }
                    index++;
                }

                return Messages.SecurityUserDoesNotExist;
            }
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

        public List<User> GetUsers()
        {
            return users;
        }

        public List<Profile> GetProfiles()
        {
            return profiles;
        }

        public bool CheckPrivilege(User user, PrivilegeType privilege, string table)
        {
            if (user.GetName() == "admin")
            {
                return true;
            }
            else
            {
                Profile pr = user.GetProfile();
                if (pr.GetHashTable().ContainsKey(table))
                {
                    if (pr.GetHashTable()[table].Contains(privilege))
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }

        public bool CheckPassword(string username, string password)
        {
            User u = GetUser(username);
            if(u != null)
            {
                if(u.GetPassword() == password || (username == "admin" && password == "admin"))
                {
                    return true;
                }
            }
            
            return false;
        }

        public User GetUser(string name)
        {
            User us=null;
            foreach (User u in users)
            {
                if (u.GetName() == name)
                {
                    us=u;
                }
            }

            return us;
        }
        public void SaveSecurity(string Directory)
        {
            string SecurityDirectoryUsers = Directory + "\\" + "User";
            string[] values=null;
            int i = 1;
            foreach (User u in users)
            {
                values[i] = ""+u.GetName() + "[[delimiter]]" + u.GetPassword() + "[[delimiter]]" + u.GetProfile();
                
            }
            File.WriteAllLines(SecurityDirectoryUsers, values);

            string[] valuesProfile = null;
            string SecurityDirectoryProfiles = Directory + "\\" + "Profile";
            foreach (Profile p in profiles)
            {
                foreach (p.GetHashTable())
                {
                    valuesProfile[p] = "" + p.GetName + "";
                }
            }
            File.WriteAllLines(SecurityDirectoryProfiles, valuesProfile);
            
        }
    }
    
    
}
