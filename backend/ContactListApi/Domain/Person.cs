using System;
using System.Collections.Generic;

namespace ContactList.Domain
{
    public class Person
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public List<Contact> Contacts { get; set; }

        public Person()
        {
            Id = Guid.NewGuid();
            Name = string.Empty;
            Sex = string.Empty;
            Contacts = new List<Contact>();
        }

        public Person(Guid? id, string name, string sex, int age)
        {
            Id = id == null?Guid.NewGuid():id;
            Name = name;
            Sex = sex;
            Age = age;
            Contacts = new List<Contact>();
        }
    }
}
