CREATE OR ALTER PROCEDURE dbo.GetAllEmployees
AS
BEGIN
    SELECT * FROM Employees WHERE IsArchived = 0;
END;
GO