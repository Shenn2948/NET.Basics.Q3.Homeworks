CREATE PROCEDURE [dbo].[spEmployee_InsertInfo]
	@EmployeeName nvarchar(100) = NULL, 
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(50) = NULL,
	@CompanyName nvarchar(20) = NULL,
	@Position nvarchar(50) = NULL,
	@Street nvarchar(50) = NULL,
	@City nvarchar(20) = NULL,
	@State nvarchar(50) = NULL,
	@ZipCode nvarchar(50) = NULL
AS
BEGIN
	IF(RTRIM(ISNULL(@EmployeeName, '')) LIKE '')
	BEGIN
		RAISERROR ('Invalid parameter: @EmployeeName cannot be null, empty or whitespace', 16, 1)
		RETURN
	END

	IF(LEN(@CompanyName) > 20)
	BEGIN
		SET @CompanyName = LEFT(@CompanyName, 20);
	END

	DECLARE @AddressId INT;
	DECLARE @PersonId INT;

	INSERT INTO Address(Street, City, State, ZipCode) VALUES(@Street, @City, @State, @ZipCode)
	SET @AddressId = @@IDENTITY;

	INSERT INTO Person(FirstName, LastName) VALUES(@FirstName, @LastName)
	SET @PersonId = @@IDENTITY;

	INSERT INTO Company(Name, AddressId) VALUES(@CompanyName, @AddressId)

	INSERT INTO Employee(AddressId, PersonId, CompanyName, Position, EmployeeName)
	VALUES(@AddressId, @PersonId, @CompanyName, @Position, @EmployeeName);
END
