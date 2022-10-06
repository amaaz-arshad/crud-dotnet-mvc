# crud-dotnet-mvc
This is an Employee CRUD project with ASP.NET MVC and SQL Server.

There are 4 tables in the application: Employees, Designation, Department, and Bank.

User Defined Constraints:

-> This is a current employee database.
-> Employee table has employee_name, designation, salary, address, joining_date, account_no, bank.
-> Employee can have only 1 designation at a time.
-> Employee can have only 1 bank account provided to the company.
-> Multiple employees can have account in the same bank.
-> Employee can be assigned to only 1 department.


-> Department table has dept_name and manager assigned to each department.
-> Each department can have atmost 1 manager.
-> A manager can belong to atmost 1 department.
-> A department can have multiple designations under it.


-> Designation table has designation and department assigned to it.
-> Each designation can belong to only 1 department
-> Multiple employees can have same designation.




