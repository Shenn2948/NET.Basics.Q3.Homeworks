BEGIN TRANSACTION  

EXEC [dbo].[spEmployee_InsertInfo]
	@EmployeeName = 'Kevin Smith',
	@FirstName = 'Kevin',
	@LastName = 'Smith',
	@CompanyName  = 'ABC Company',
	@Position = 'manager',
	@Street  = '1 Street',
	@City  = 'Washington',
	@State  = 'Washington',
	@ZipCode = '444'

IF EXISTS (SELECT TOP (1) * FROM dbo.Person WHERE [FirstName] = 'Kevin')
   PRINT ('Success - insert Employee')

IF EXISTS (SELECT TOP (1) c.Name FROM dbo.Company c WHERE c.Name  = 'ABC Company')
   PRINT ('Success - insert Company')

ROLLBACK TRANSACTION