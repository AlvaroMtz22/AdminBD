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
        Profile m_profile;

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
        


        

    }
    

  
    
}
