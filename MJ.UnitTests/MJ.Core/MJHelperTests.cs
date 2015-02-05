using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatthewJesse;
using SharpTestsEx;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using MJ.Lib.UnitTests.FakeViewModels;


namespace MJ.Lib.UnitTests.MJ
{
    [TestFixture]
    class MJHelperTests
    {
        protected TextBox txtBox;
        protected TextBox txtBox1;
        protected PictureBox picBox;
        protected ListView lstView;
        protected Button btnTest;
        [SetUp]
        public void prepareUI()
        {
            txtBox = new TextBox();
            txtBox.Text = "test";

            txtBox1 = new TextBox();
            txtBox1.Text = "test";

            picBox = new PictureBox();

            lstView = new ListView();

            btnTest = new Button();
        }
        [Test]
        public void FUI_Text_Should_change()
        {
            //arrange 
            var check = new CheckModel();
            check.Id = "change tile";
            check.ABA = "Aba";
            txtBox.TextBindTo<CheckModel>(m => m.Id);

            //act
            txtBox.Load<CheckModel>(check);

            //assert
            txtBox.Text
                .Should()
                .Be
                .EqualTo(check.Id);
        }
        [Test]
        public void FUI_Text1_Should_not_changed()
        {
            //arrange
            var check = new CheckModel();
            check.Id = "change tile";

            var deposit = new DepositModel();
            deposit.Id = "Deposit Id";

            txtBox.TextBindTo<CheckModel>(m => m.Id);
            txtBox1.TextBindTo<DepositModel>(m => m.Id);

            //act
            txtBox1.Load<DepositModel>(deposit);

            //assert
            txtBox1.Text
                .Should()
                .Be
                .EqualTo(deposit.Id);
        }
        [Test]
        public void FUI_Should_get_sum_amount_of_checks()
        {
            //arrange
            var checklist = new List<CheckModel>();
            var check1 = new CheckModel();
            var check2 = new CheckModel();

            check1.Amount = check2.Amount = 888;
            
            checklist.Add(check1);
            checklist.Add(check2);

            txtBox.TextFor<List<CheckModel>>(m =>
            {
                decimal amount = 0;
                int count = 0;
                foreach (var c in m)
                {
                    amount += c.Amount;
                    count++;
                }
                return string.Format("{0:C2}[{1}]", amount, count);
            });

            //act
            txtBox.Load<List<CheckModel>>(checklist);

            //assert
            txtBox.Text.Substring(0, 6)
                .Should()
                .Be
                .EqualTo("$1,776");

        }

        [Test]
        public void FUI_model_value_Should_change_with_Control_value()
        {
            //arrange
            var check = new CheckModel();
            check.Id = "test1";

            txtBox.TextBindTo<CheckModel>(m => m.Id);

            //act
            txtBox.Load<CheckModel>(check);

            //assert
            txtBox.Text.Should()
                .Be.EqualTo("test1");

            //arrange
            txtBox.Text = "test2";

            //act
            check.Get<CheckModel>();

            //arrange
            check.Id
                .Should()
                .Be
                .EqualTo("test2");

        }
        [Test]
        public void FUI_add_items_to_ListView()
        {
            //arrange
            var checklist = new List<CheckModel>();

            var check1 = new CheckModel();
            var check2 = new CheckModel();

            check1.ABA = check2.ABA = "aba";
            check1.CheckNumber = check2.CheckNumber = "check111";
            check1.AccountNumber = check2.AccountNumber = "777";
            check1.Amount = check2.Amount = 888;

            checklist.Add(check1);
            checklist.Add(check2);

            lstView.ListFor<List<CheckModel>>((v, l) => CreateMyListView1(v, l));

            lstView.Load<List<CheckModel>>(checklist);

            lstView
                .Satisfy(c =>
                    c.Items.Count == 2);

        }
        protected void CreateMyListView1(ListView lstView, List<CheckModel> list)
        {
            if (list.Count() == 0)
                lstView.Items.Clear();
            int index = 1;
            foreach (var im in list)
            {
                var items = new List<ListViewItem>();

                var item1 = new ListViewItem(index.ToString(), (im.Status >= 0 && im.Status < 4) ? im.Status : 0);
                item1
                    .SubItems
                    .Add(string.Format("{0:C2}", im.Amount));
                item1
                    .SubItems
                    .Add(string.Format("{0}|{1}:|{2}", im.ABA, im.AccountNumber, im.CheckNumber));
                item1
                    .SubItems
                    .Add(string.Format("X", 30));

                items.Add(item1);
                im.Index = index.ToString();
                index++;
                lstView.Items.AddRange(items.ToArray());

            }
            lstView.Invalidate();
        }
        [Test]
        public void FUI_Add_image_to_picturebox()
        {
            var check = new CheckModel();
            check.FrontImage = Image.FromFile("ImageData\\image_front.jpg");

            picBox.ImageBindTo<CheckModel>(m => m.FrontImage);

            picBox.Load<CheckModel>(check);

            picBox.Image
                .Should()
                .Be
                .InstanceOf<Image>();

        }
        [Test]
        public void FUI_get_image_from_picturebox()
        {
            var check = new CheckModel();

            picBox.ImageBindTo<CheckModel>(m => m.FrontImage);

            picBox.Image = Image.FromFile("ImageData\\image_front.jpg");


            check.Get<CheckModel>();

            check.FrontImage
                .Should()
                .Be
                .InstanceOf<Image>();

        }

        [Test]
        public void FUI_Button_Should_be_disable()
        {
            //arrange
            var scanner = new ScannerViewModel();
            scanner.Text = "this is test";
            scanner.buttonScan = false;

            btnTest.EnabledBindTo<ScannerViewModel>(m => m.buttonScan);
            btnTest.TextBindTo<ScannerViewModel>(m => m.Text);

            //act
            btnTest.Load<ScannerViewModel>(scanner);

            //assert
            btnTest.Satisfy(c =>
                c.Enabled == false &&
                c.Text == "this is test"
                );
        }
        [Test]
        public void FUI_get_Status_from_Button()
        {
            //arrange
            var scanner = new ScannerViewModel();
            btnTest.EnabledBindTo<ScannerViewModel>(m => m.buttonScan);

            //act
            btnTest.Enabled = false;
            scanner.Get<ScannerViewModel>();

            //assert
            scanner.buttonScan
                .Should().Be.EqualTo(false);

            //act
            btnTest.Enabled = true;
            scanner.Get<ScannerViewModel>();

            //assert
            scanner.buttonScan
                .Should().Be.EqualTo(true);
        }
        [Test]
        public void FUI_Text_Should_be_validate_success()
        {
            var check = new CheckModel();
            check.Id = "change tile";
            check.ABA = "Aba";

            txtBox.TextBindTo<CheckModel>(m => m.Id)
                  .ToValidate(m =>
                      m.Id.Length > 5)
                  .ToSuccess((c, m) =>
                  {
                      c.BackColor = Color.Yellow;
                  })
                  .ToFail((c, m) =>
                  {
                      c.BackColor = Color.Red;
                  });


            txtBox.Load<CheckModel>(check);

            txtBox.Satisfy<TextBox>(c =>
                c.Text == check.Id &&
                c.BackColor == Color.Yellow);

        }
        [Test]
        public void FUI_Text_Should_be_validate_failed()
        {
            var check = new CheckModel();
            check.Id = "change tile";
            check.ABA = "Aba";

            txtBox.TextBindTo<CheckModel>(m => m.Id)
                  .ToValidate(m =>
                      m.Id.Length <= 5)
                  .ToSuccess((c, m) =>
                  {
                      c.BackColor = Color.Yellow;
                  })
                  .ToFail((c, m) =>
                  {
                      c.BackColor = Color.Red;
                  });


            txtBox.Load<CheckModel>(check);

            txtBox.Satisfy<TextBox>(c =>
                c.Text == check.Id &&
                c.BackColor == Color.Red);
        }

        class ExpressionUITests : MJHelperTests
        {
            [Test]
            public void Expression_Should_get_Member_name()
            {
                //arrange 
                Expression<Func<CheckModel, string>> expr = m => m.Id;

                //act
                var expression = GetMemberInfo(expr);

                var name = expression.Member.Name;

                //assert
                name.Should()
                    .Be.EqualTo("Id");

            }
            [Test]
            public void Expression_Should_can_set_Member_value()
            {
                //arrange 
                Expression<Func<CheckModel, string>> expr = m => m.Id;

                //act
                var expression = GetMemberInfo(expr);

                var name = expression.Member.Name;

                var expected = new CheckModel();
                PropertyInfo prop = expected.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                {
                    prop.SetValue(expected, "test001", null);
                }
                //assert
                expected.Id.Should()
                    .Be.EqualTo("test001");

            }
            private static MemberExpression GetMemberInfo(Expression method)
            {
                LambdaExpression lambda = method as LambdaExpression;
                if (lambda == null)
                    throw new ArgumentNullException("method");

                MemberExpression memberExpr = null;

                if (lambda.Body.NodeType == ExpressionType.Convert)
                {
                    memberExpr =
                        ((UnaryExpression)lambda.Body).Operand as MemberExpression;
                }
                else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
                {
                    memberExpr = lambda.Body as MemberExpression;
                }

                if (memberExpr == null)
                    throw new ArgumentException("method");

                return memberExpr;
            }
        }

    }
}
