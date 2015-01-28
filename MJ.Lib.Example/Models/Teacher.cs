using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Lib.Example.Models
{
    class Teacher : Person
    {
        public string Title { get; set; }
        public Course[] Courses { get; set; }
        Teacher(long id, string lName, string fName, DateTime birthDate, bool gender, string title, string adress, string phone, string hobby)
        {
            ID = id;
            LastName = lName;
            FirstName = fName;
            BirthDate = birthDate;
            Gender = gender;
            Title = title;
            Address = adress;
            ContactNo = phone;
            Hobby = hobby;
        }
    }
}
