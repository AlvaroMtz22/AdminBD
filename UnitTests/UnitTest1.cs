using Microsoft.VisualStudio.TestTools.UnitTesting;
using Buscamineros;
namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void toString()
        {
            Contact contact1 = new Contact("Ronny", "rc@gmail.com", "4656");
            string asString = contact1.ToString();
            Assert.IsTrue(asString.Contains("Ronny"));
        }
        [TestMethod]
        public void AddandCount()
        {
            Agenda agenda = new Agenda();
            Contact contact1 = new Contact("Ronny", "rc@gmail.com", "4656");
            Contact contact2 = new Contact("Rony", "rc@gmil.com", "456");
            agenda.addContact(contact1);
            agenda.addContact(contact2);

            Assert.AreEqual(2, agenda.Count());
        }
    }
}
