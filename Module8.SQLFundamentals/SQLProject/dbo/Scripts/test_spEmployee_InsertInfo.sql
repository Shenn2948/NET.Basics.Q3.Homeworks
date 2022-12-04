BEGIN TRANSACTION  

EXEC [dbo].[spEmployee_InsertInfo]
	@EmployeeName = 'Kevin Smith',
	@FirstName = 'Kevin',
	@LastName = 'Smith',
	@CompanyName  = 'Company ABC',
	@Position = 'manager',
	@Street  = '1 Street',
	@City  = 'Washington',
	@State  = 'Washington',
	@ZipCode = '444'

IF EXISTS (SELECT TOP (1) * FROM dbo.Person WHERE [FirstName] = 'Kevin')
   PRINT ('Employee with name Kevin was created')

IF EXISTS (SELECT TOP (1) * FROM dbo.Company c WHERE c.Name = 'Company ABC')
   PRINT ('Company was created')

ROLLBACK TRANSACTION