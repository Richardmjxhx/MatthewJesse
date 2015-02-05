using MJ.Lib.UnitTests.FakeViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Lib.UnitTests.FakeController
{
    public class MainController
    {
        public object[] Test(int id,int index)
        {
            var check = new CheckModel();
            check.Id = id.ToString();

            return new object[]{check};
        }
        public object[] TestNull(int id,int index)
        {
            var check = new CheckModel();
            check.Id = id.ToString();

            return null;
        }
        public object[] Test1(string id, int index)
        {
            var check = new CheckModel();
            check.Id = id.ToString();

            return new object[]{check};
        }

    }
    public class TestController
    {
        public object[] Test(string id,int index )
        {
            var check = new CheckModel();
            check.Id = id.ToString() + "TestController";

            return new object[] { check };
        }

    }
}
