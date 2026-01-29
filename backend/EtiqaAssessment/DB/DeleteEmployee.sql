CREATE OR ALTER PROCEDURE dbo.DeleteEmployee
--PARAMETERS--
@EmployeeNumber NVARCHAR(20)
AS
BEGIN

	--Update employee--
	UPDATE Employees SET 
		IsArchived = 1
	WHERE EmployeeNumber = @EmployeeNumber

END;
GO