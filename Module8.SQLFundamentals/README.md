# SQL Fundamentals

## Self-study materials

- Programming Foundations: Databases (1h 25m)
https://www.linkedin.com/learning/programming-foundations-databases-2
- SQL: DDL, DQL, DML, DCL and TCL commands (10m)
https://www.geeksforgeeks.org/sql-ddl-dql-dml-dcl-tcl-commands
- SQL Stored Procedures (30m)
  - https://www.linkedin.com/learning/microsoft-sql-server-2019-essential-training/introduction-to-stored-procedures
  - https://www.linkedin.com/learning/microsoft-sql-server-2019-essential-training/create-stored-procedures
  - https://www.linkedin.com/learning/microsoft-sql-server-2019-essential-training/parameterized-stored-procedures
https://youtu.be/Sggdhot-MoM?t=1194
- SQL Server Database Triggers – Trigger Fundamentals(29m)
https://www.linkedin.com/learning/sql-server-database-triggers/what-are-sql-server-triggers
- Basic of indexes (22m)
https://www.linkedin.com/learning/microsoft-sql-server-2016-essential-training/basics-of-indexes
- Create a view (7m)
https://www.linkedin.com/learning/microsoft-sql-server-2016-essential-training/create-a-view
- How to: Create a New Database Project (3m)
https://docs.microsoft.com/en-us/sql/ssdt/how-to-create-a-new-database-project
- How To Create SQL Server Database Project with Visual Studio (20m)
https://www.c-sharpcorner.com/article/how-to-create-sql-server-database-project-with-visual-studio
- SQL Data Tools in C# - Database Creation, Management, and Deployment in Visual Studio 2017 (50m)
https://www.youtube.com/watch?v=ijDcHGxyqE4

### Additional:

- Microsoft samples databases
https://github.com/microsoft/sql-server-samples/tree/master/samples/databases
- SQL for non-programmers (1h 30m)
https://www.linkedin.com/learning/sql-for-non-programmers
- Microsoft SQL Server 2019 Essential Training (4h 30m)
https://www.linkedin.com/learning/microsoft-sql-server-2019-essential-training/harness-the-modern-and-innovative-tools-in-sql-server-2019
- SQL Essential Training (3h)
https://www.linkedin.com/learning/sql-essential-training-3
- SQL Server Database Triggers (1h 39m)
https://www.linkedin.com/learning/sql-server-database-triggers
- SQL Server Data Tools for Visual Studio (52m)
https://www.youtube.com/watch?v=9WH_r7M4QhM
- Relational Databases Essential Training (2h 12m)
https://www.linkedin.com/learning/relational-databases-essential-training/organize-data-with-the-relational-model?u=2113185
- SQL Server Management Studio (SSMS) | Full Course (1h 22m)
https://www.youtube.com/watch?v=Q8gBvsUjTLw

## HomeWork
### Task 1:

Create a SQL DB project in VS2019 with the structure of the following tables:

- Person
  1. Id, int, not NULL, PK
  1. FirstName, nvarchar(50), not NULL
  1. LastName, nvarchar(50), not NULL
- Address
  1. Id, int, not NULL, PK
  1. Street, nvarchar(50), not NULL
  1. City, nvarchar(20), NULL
  1. State, nvarchar(50), NULL
  1. ZipCode, nvarchar(50), NULL
- Employee
  1. Id, int, not NULL, PK
  1. AddressId, int, not NULL, FK (Address.Id)
  1. PersonId, int, not NULL, FK (Person.Id)
  1. CompanyName, nvarchar(20), not NULL
  1. Position, nvarchar(30), NULL
  1. EmployeeName, nvarchar(100), NULL
- Company
  1. Id, int, not NULL, PK
  1. Name, nvarchar(20), not NULL
  1. AddressId, int, not NULL, FK (Address.Id)

Publish DB into SQL Server with default information (create Script.postdeploy*.sql and fill once all tables with the appropriate data)

### Task 2:

Create ‘EmployeeInfo’ view to show data with the following structure that is sorted by ‘CompanyName, City’ ASC:

- EmployeeId,
- EmployeeFullName (EmployeeName (if not null) or ‘{FirstName} {LastName}’),
- EmployeeFullAddress(‘{ZipCode}_{State}, {City}-{Street}’)
- EmployeeCompanyInfo(‘{CompanyName}({Position})’)

### Task 3:

Create a stored procedure to insert Employee info into DB with the following params:

- EmployeeName(optional)
- FirstName(optional)
- LastName(optional)
- CompanyName(required)
- Position(optional)
- Street(required)
- City(optional)
- State(optional)
- ZipCode(optional)

And the following conditions:

- At least one field (either EmployeeName  or FirstName or LastName) should be not be:
  - NULL
  - empty string
  - contains only ‘space’ symbols
- CompanyName should be truncated if length is more than 20 symbols

### Task 4:

Create a trigger for Employee table on insert new Row that will create a new Company with an Address (The address should be copied from the employee’s address).