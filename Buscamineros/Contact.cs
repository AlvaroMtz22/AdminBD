using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
     public class Contact
    {
        private string m_Name;
        private string m_Email;
        private string m_Phone;

        public Contact(string name, string email= null, string phone = null){
            m_Name = name;
            m_Email = email;
            m_Phone = phone;
        }

        public override string ToString()
        {
            return m_Name + ", " + m_Email + ", " + m_Phone; ;
        }

    }
}
