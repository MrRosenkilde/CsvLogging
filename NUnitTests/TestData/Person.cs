using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.TestData
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Person(string firstName, string lastName) 
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
