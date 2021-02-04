using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buscamineros
{
    class Program
    {
        static void Main(string[] args)
        {
            Agenda agenda = new Agenda(); 
            Contact contact1 = new Contact("Ronny","Ronny@gmail.com","123456789");
            Contact contact2 = new Contact("Aitor", "Aitor@gmail.com", "123456789");
            Contact contact3 = new Contact("Unai", "Unai@gmail.com", "123456789");
            Contact contact4 = new Contact("Alvaro", "Alvaro@gmail.com", "123456789"); 
            agenda.addContact(contact1);
            agenda.addContact(contact2);
            agenda.addContact(contact3);
            agenda.addContact(contact4);
            agenda.print();


        }
    }
}
