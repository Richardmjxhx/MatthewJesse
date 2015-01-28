using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Lib.Example.Models
{
    class Student:Person
    {
        public int Grade { get; set; }
        public Course[] Courses { get; set; }
        Student(long id,string lName,string fName,DateTime birthDate,bool gender,int grade, string adress, string phone,string hobby)
        {
            ID = id;
            LastName = lName;
            FirstName = fName;
            BirthDate = birthDate;
            Gender = gender;
            Grade = grade;
            Address = adress;
            ContactNo = phone;
            Hobby = hobby;
        }
    }
}
