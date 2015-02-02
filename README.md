# MatthewJesse
MatthewJesse is a individual C# project, intends to provide a MVC framework for C# Winform application developments, it includes 
two parts: MJ.Core and MJ.MVC, MJ.Core doese data binding part for MJ.MVC framework, it can be used separately, or comes with MJ
.MVC.

With MJ.Core, Winform developers can bind data model to UI controls, then MJ.Core will push/pull data model to/from controls   
automatically, actually, there are two types of data binding in MJ.Core, one-way-binding and two-way-binding, one-way-binding only
supports push data to controls, but can't get data from controls  two-way-binding means you can do both of them. pros and cons of
this two ways, see examples below.

MJ.MVC provides complete MVC framework for Winform application developments, it is lightweight, painlessly turn on/off, the only
prerequisite to use MJ.MVC is there need has at least one 'public' class named with "Controller" suffix, that's why I am saying
"painlessly", and benefits are tremendous, such as, easy to apply Unit Test/TDD, makes your code neat and beautiful,etc. more 
details please see examples below.

In one word, MJ.MVC/MJ.Core is a fantastic choice for C# Winform application developments particularly when you plan to apply TDD
or do Unit tests, no matter your project status is waiting-to-start or already in-process.
