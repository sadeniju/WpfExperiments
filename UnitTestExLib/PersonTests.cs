using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnderTestExample;

namespace UnitTestExample {
    [TestFixture]
    public class PersonTests {
        Person person;

        [SetUp]
        public void SetUp() {
            // the setup method is executed before _EACH_ test
            person = new Person { FirstName="Sarah", MiddleName="Desiree Nicole Julia", LastName="Doe" };
        }

        //[TearDown]
        //public void TearDown() {
        //    person = null;
        //}

        [Test]
        public void IsValidFullName_FullName_ReturnsTrue() {
            string fullName = person.GetFullName();
            string expectedFullName = "Sarah Desiree Nicole Julia Doe";
            Assert.AreEqual(fullName, expectedFullName, "The full name does not have the expected value.");
        }

        [Test]
        [ExpectedException(typeof(MissingFieldException))]
        public void IsValidFullName_EmptyLastName_ThrowsException() {
            person.LastName = "";
            person.GetFullName();
        }
    }
}
