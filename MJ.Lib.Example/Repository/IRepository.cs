using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MJ.Lib.Example.Repository
{
    enum ObjectType
    {
        TEACHER = 0,
        STUDENT,
        COURSE
    }
    interface IRepository
    {
        object FindOne(ObjectType type, long id);
        object[] GetAll(ObjectType type);
        bool Update(ObjectType type, object ob);
    }
}
