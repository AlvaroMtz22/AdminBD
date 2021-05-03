using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Buscamineros
{
   public class User 
    {
        private string m_username;
        private string m_password;
        Profile m_profile;

        public User (string username, string password, Profile profile)
        {
            m_username = username;
            m_password = password; 
            m_profile = profile;
        }

        public string GetName()
        {
            return m_username;
        }

        public Profile GetProfile()
        {
            return m_profile;
        }

        public void SetProfile(Profile profile)
        {
            m_profile = profile;
        }
    }
}
