/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT TOP (1) * FROM dbo.Person WHERE [FirstName] = 'Adam')
BEGIN 
    INSERT INTO Person(FirstName, LastName) VALUES ('Adam', 'Johnson')
    INSERT INTO Person(FirstName, LastName) VALUES ('Bill', 'Adams')
    INSERT INTO Person(FirstName, LastName) VALUES ('Rob', 'Williams')
END

IF NOT EXISTS (SELECT TOP (1) * FROM dbo.Address WHERE [Street] = '55 Street')
BEGIN 
    INSERT INTO Address(Street, City, State, Zipcode) VALUES('55 Street', 'Chicago', 'Illinois', '111')
    INSERT INTO Address(Street, City, State, Zipcode) VALUES('22 Street', 'San-Francisco', 'California', '222')
    INSERT INTO Address(Street, City, State, Zipcode) VALUES('11 Street', 'Boston', 'Massachusetts', '333')
END

IF NOT EXISTS (SELECT TOP (1) * FROM dbo.Company WHERE [Name] = 'Company 1')
BEGIN 
    INSERT INTO Company(Name, AddressId) VALUES('Company 1', 1)
    INSERT INTO Company(Name, AddressId) VALUES('Company 2', 2)
    INSERT INTO Company(Name, AddressId) VALUES('Company 3', 3)
END

IF NOT EXISTS (SELECT TOP (1) * FROM dbo.Employee WHERE [EmployeeName] = 'Adam Johnson')
BEGIN 
    INSERT INTO Employee(AddressId, PersonId, CompanyName, Position, EmployeeName) VALUES(1, 1, 'Company 1', 'developer', 'Adam Johnson')
    INSERT INTO Employee(AddressId, PersonId, CompanyName, Position, EmployeeName) VALUES(2, 2, 'Company 2', 'developer', 'Bill Adams')
    INSERT INTO Employee(AddressId, PersonId, CompanyName, Position, EmployeeName) VALUES(3, 3, 'Company 3', 'developer', 'Rob Williams')
END