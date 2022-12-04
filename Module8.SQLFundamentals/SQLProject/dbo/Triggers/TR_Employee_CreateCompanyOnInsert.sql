CREATE TRIGGER [TR_Employee_CreateCompanyOnInsert]
ON [dbo].[Employee]
AFTER INSERT
AS
BEGIN
    DECLARE @CompanyName NVARCHAR(20), @AddressId INT;

    SELECT @CompanyName = e.CompanyName, @AddressId = e.AddressId
    FROM Employee e
    WHERE e.Id=@@IDENTITY

    IF NOT EXISTS (SELECT TOP (1) [AddressId] FROM dbo.Company WHERE [AddressId] = @AddressId)
    BEGIN 
        INSERT INTO Company(Name, AddressId) VALUES (@CompanyName, @AddressId);
    END
END
