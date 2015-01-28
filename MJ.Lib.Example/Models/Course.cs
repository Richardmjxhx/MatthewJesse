using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Lib.Example.Models
{
    class Course
    {
        public long ID { get; set; }
        public Teacher[] Teachers { get; set; }
        public string Description { get; set; }
        Course(long Id, string description)
        {
            ID = Id;
            Description = description;
        }
    }
}
