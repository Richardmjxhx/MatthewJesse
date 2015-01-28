using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MJ.Lib.Example.Models
{
    class Person
    {
        public long ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Gender { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Hobby { get; set; }
    }
}
