using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    class User 
    
    //name, password, profile (profile class)
    {
        
        private string m_username;
        private System.Security.SecureString m_password;
        private Profile m_profile = new Profile();

        public User (string username, System.Security.SecureString password, Profile profile)
        {

            m_username = username;
            m_password = password;
            m_profile = profile;
        }
        public string GetName()
        {
            return m_username;
        }
        

        //Dictionary<string,List<Privilege>>
        //table Profile = new Table();
        //tableProfile["User Profiles"] = new List<Privilege>();
        //tableProfile["User Profiles"].add(PrivilegeDelete);
        //tableProfile["User Profiles"].add(PrivilegeInsert);
        //tableProfile["User Profiles"].add(PrivilegeSelect);
        //tableProfile["User Profiles"].add(PrivilegeUpdate);

    }
    

  
    
}
