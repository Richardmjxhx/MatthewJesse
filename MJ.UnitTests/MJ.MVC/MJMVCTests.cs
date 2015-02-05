using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MatthewJesse;
using MatthewJesse.MVC;
using MJ.Lib.UnitTests.FakeViewModels;
using SharpTestsEx;
using System.Reflection;

namespace MJ.Lib.UnitTests.MJ.MVC
{
    [TestFixture]
    class MJMVCTests
    {
        private Form testForm;
        private TextBox txtBox;

        [SetUp]
        public void PrepareForTest()
        {
            testForm = new Form();

            txtBox = new TextBox();
            txtBox.Location = new Point(25,25);

            testForm.Controls.Add(txtBox);            
        }

        [TestCase("","Test",1,0,"1")]
        [TestCase("", "TestNull", 1, 0, "")]
        [TestCase("TestController", "Test", "1", 0, "1TestController")]
        [Test]
        public void MJ_Methods_Should_can_map_to_Controller(string controller, string methedName, object input1, object input2, string expected)
        {
            //arrange 
            txtBox.TextBindTo<CheckModel>(m => m.Id);

            //act
            testForm._MJ_(controller, methedName, input1,input2);

            //assert
            txtBox.Text
                .Should()
                .Be
                .EqualTo(expected);

        }
        [Test]
        public void MJ_calling_generic_type_Extention_Method_Load_Should_success()
        {
            //arrange 
            var check = new CheckModel();
            check.Id = "change tile";
            txtBox.TextBindTo<CheckModel>(m => m.Id);

            var method = typeof(_MJ_Helper).GetMethod("Load");
            var genericMethod = method.MakeGenericMethod(new Type[] { check.GetType() });

            //act
            genericMethod.Invoke(null, new object[] { txtBox,check });

            //assert
            txtBox.Text
                .Should()
                .Be
                .EqualTo(check.Id);
        }

        [Test]
        public void Should_can_get_Controller_from_current_Assembly()
        {
            //act
            var asm = Assembly.GetExecutingAssembly();
            
            var types =from t in asm.GetTypes()
                        where t.IsClass &&
                        t.IsPublic &&
                        t.Name.ToUpper().Equals("MAINCONTROLLER")
                        select t;
            //assert
            var count = types.Count();
            types.ElementAt(0).Name.Should()
                .Be.EqualTo("MainController");

        }
        [Test]
        public void Should_can_get_Controller_method_from_current_Assembly()
        {
            //act
            var asm = Assembly.GetExecutingAssembly();

            var types = from t in asm.GetTypes()
                        where t.IsClass &&
                        t.IsPublic &&
                        t.Name.ToUpper().Contains("MAINCONTROLLER")
                        select t;

            object classInstance = Activator.CreateInstance(types.ElementAt(0), null);

            var method = types.ElementAt(0).GetMethod("Test1", new Type[] { typeof(string), typeof(int) });

            object[] parametersArray = new object[] { "123", 11 };
            object[] retvalues =(object[]) method.Invoke(classInstance, parametersArray);
            CheckModel check = null;
            
            if (retvalues[0].GetType() == typeof(CheckModel))
            {
                check = (CheckModel)retvalues[0];
            }
            //assert
            check.Id.Should()
                .Be.EqualTo("123");

        }

    }
}
