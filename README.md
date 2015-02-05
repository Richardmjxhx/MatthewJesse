# MatthewJesse

MatthewJesse is an individual C# project, intends to provide a MVC framework for C# Winform application developments, 
it includes two parts: MJ.Core and MJ.MVC, MJ.Core doese data binding part for MJ.MVC framework, it can be used
separately, or comes with MJ.MVC.

With MJ.Core, Winform developers can bind data model to UI controls, then MJ.Core will push/pull data model to/from
controls automatically, actually, there are two types of data binding in MJ.Core, one-way-binding and two-way-binding,
one-way-binding only supports push data to controls, but can't get data from controls  two-way-binding means you can do
both of them. pros and cons of this two ways, see examples below.

MJ.MVC provides complete MVC framework for Winform application developments, it is lightweight, painlessly turn on/off, 
the only prerequisite to use MJ.MVC is there need has at least one 'public' class named with "Controller" suffix, 	
that's why I am saying "painlessly", and benefits are tremendous, such as, easy to apply Unit Test/TDD, makes your code 
neat and beautiful,etc. more details please see examples below.

In one word, MJ.MVC/MJ.Core is a fantastic choice for C# Winform application developments particularly when you plan to 
apply TDD or do Unit tests, no matter your project status is waiting-to-start or already in-process.

# Code Examples

  MJ.Core examples

    1.1 Data binding

       two-way-binding code sinppet:

	        /* binding a TextEdit control with DepositModel 'accountName' attri */
	        txtAccount.TextBindTo<DepositModel>(m => m.accountName);

       one-way-binding code sinppet:

	        /* binding Total Amount TextEdit with delegate, which depends on DepositModel */
	        txtTotalAmount.TextFor<DepositModel>(m =>{
	        var amount = 0;
	        ...
	        return amount;
	        });

    1.2 Data Load

	code sinppet:

        /* txtBox1 can be any of control instances */
        txtBox1.Load<DepositModel>(deposit);

	According data bindings, Load() will push deposit data to all controls binding to DepositModel.


    1.3 Data Get

	code sinppet:

        var check = new CheckModel();
        check.Get<CheckModel>();

	According data bindings, Get() will pull check data from all controls binding to CheckModel.

    1.4 More features with Data binding

	MJ.Core supports more complex features coming with data binding, such as Validation and Ignorable Conditions.

	Validation:

        txtTotal.TextBindTo<DepositModel>(m => m.actualAmount)
                        .ToValidate(m => m.actualAmount <= m.expectedAmount)
                        .ToSuccess((c, m) =>
                        {
                            c.ForeColor = SystemColors.MenuHighlight;
                        })
                        .ToFail((c, m) =>
                        {
                            c.ForeColor = Color.Red;
                        });

	Ignorable Condition:

        txtCheksNum.TextBindTo<DepositModel>(m => m.actualNumberOfChecks)
                   .ToIgnore(m => m.status == PENDING);

    1.5 Complete example
        
        ...
        /*Data binding for DepositModel*/
        txtAccount.TextBindTo<DepositModel>(m => m.accountName);

        txtLocation.TextBindTo<DepositModel>(m => m.locationName);

        txtTotal.TextBindTo<DepositModel>(m => m.actualAmount)
                        .ToValidate(m => m.actualAmount <= m.expectedAmount)
                        .ToSuccess((c, m) =>
                        {
                            c.ForeColor = SystemColors.MenuHighlight;
                        })
                        .ToFail((c, m) =>
                        {
                            c.ForeColor = Color.Red;
                        });

        txtCheksNum.TextBindTo<DepositModel>(m => m.actualNumberOfChecks)
                    .ToIgnore(m => m.status == PENDING);
        
        ...
        /*Push data to controls*/
        var deposit = GetDepositDataFromWherever();
        txtAccount.Load<DepositModel>(deposit);

        ...
        /*Pull data from controls*/
        var deposit = new DepositModel();
        deposit.Get<DepositModel>();


   MJ.MVC examples

        ...
        /*Data binding for CheckModel*/
        picCheckImage.ImageBindTo<CheckModel>(m => m.CheckImage);
        ...
        /*Data binding for DepositModel*/
        txtAccount.TextBindTo<DepositModel>(m => m.accountName);
        ...
        ...
        /* Controller */
        public class WhateverController
        {
            /* insert new check to deposit, return updated deposit */
            public object[] InsertChecktoDeposit(CheckModel check)
            {
                var deposit = InsertChecktoWherever();

                return new object[] { deposit };
            }
        }
        ...
        ...
        /* WinForm(View) */
        ...
        var check = new CheckModel();
        check.Get<CheckModel>();

        this._MJ_("WhateverController", "InsertChecktoDeposit", check);
        ...

	_MJ_() method will update UI controls using return data model, In this case it will update controls binding
	to DepositModel
    

  # Features Overview
  
	 Features	            Control Property			MJ Version
	
	TextBindTo<T>()			.Text 				0.16.2 and later
	TextFor<T>()			.Text 				0.16.2 and later
	EnabledBindTo<T>()		.Enable 			0.16.2 and later
	EnabledFor<T>()			.Enable 			0.16.2 and later
	ImageBindTo<T>()		.Image 				0.16.2 and later
	ImageFor<T>()			.Image 				0.16.2 and later
	ListFor<T>()			.Items 				0.16.2 and later
	  	


    
