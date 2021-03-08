using Buscamineros.MiniSQLParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UnitTestParser
    {
        [TestMethod]
        public void Parsing()
        {
            IQuery query = Parser.Parse("SELECT * FROM Ta3ble1;");
            Assert.IsTrue(query is SelectAll);
            Assert.AreEqual("Ta3ble1", (query as SelectAll).Table());
        }
    }
}
