using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros 
{
    public class Agenda
    {
        private List<Contact> m_Contacts = new List<Contact>();

        public Agenda()
        {
        }
        public void addContact(Contact c)
        {
            m_Contacts.Add(c);
            Console.WriteLine("Contact added to Agenda");
        }
        public void print()
        { 
            foreach(Contact c in m_Contacts)
            {
                Console.WriteLine(c.ToString());
            }
        }
        public int Count()
        {
            return m_Contacts.Count;
        }

    }
}
