CREATE OR ALTER PROCEDURE dbo.UpdateEmployee
--PARAMETERS--
@EmployeeNumber NVARCHAR(20),
@FirstName NVARCHAR(200),
@LastName NVARCHAR(200),
@DateOfBirth DATE,
@WorkingDays NVARCHAR(5),
@DailyRate MONEY,
@StartDate DATE,
@EndDate DATE,
@TakeHomePay MONEY
AS
BEGIN

	--Update employee--
	UPDATE Employees SET 
		FirstName = @FirstName,
		LastName = @LastName,
		DateOfBirth = @DateOfBirth,
		WorkingDays = @WorkingDays,
		DailyRate = @DailyRate,
		StartDate = @StartDate,
		EndDate = @EndDate,
		TakeHomePay = @TakeHomePay
	WHERE EmployeeNumber = @EmployeeNumber

END;
GO