using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnderTestExample {
    public class Person {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string GetFullName() {
            if (string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(FirstName)) {
                throw new MissingFieldException();
            }
            return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
        }
    }
}
