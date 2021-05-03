﻿using Buscamineros;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class UnitTestUser
    {
        string password = "1234";
        Profile profile = new Profile("Programmer");

        [TestMethod]
        public void UserConstructorTest()
        {
            User user = new User("Unai", password, profile);
            Assert.IsNotNull(user);
        }
    }
}
