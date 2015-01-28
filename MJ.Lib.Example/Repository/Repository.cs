using MJ.Lib.Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Lib.Example.Repository
{
    class Repository : IRepository
    {
        private List<Student> _student;
        private List<Teacher> _teacher;
        private List<Course> _course;
        
        Repository()
        {
            _student = new List<Student>();
            _teacher = new List<Teacher>();
            _course = new List<Course>();
        }
        public object FindOne(ObjectType type, long id)
        {
            return null;
        }
        public object[] GetAll(ObjectType type)
        {
            return null;
        }
        public bool Update(ObjectType type, object ob)
        {
            return true;
        }

    }
}
