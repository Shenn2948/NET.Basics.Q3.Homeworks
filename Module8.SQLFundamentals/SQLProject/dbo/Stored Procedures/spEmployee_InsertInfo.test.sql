USE [DemoDB]
GO

BEGIN TRANSACTION  

EXEC [dbo].[spEmployee_InsertInfo]
	@EmployeeName = 'Kevin Smith',
	@FirstName = 'Kevin',
	@LastName = 'Smith',
	@CompanyName  = 'Long company name, greater than 20 symbols. It should be truncated',
	@Position = 'manager',
	@Street  = '1 Street',
	@City  = 'Washington',
	@State  = 'Washington',
	@ZipCode = '444'

IF EXISTS (SELECT TOP (1) * FROM dbo.Person WHERE [FirstName] = 'Kevin')
   PRINT ('Success')

ROLLBACK TRANSACTION
