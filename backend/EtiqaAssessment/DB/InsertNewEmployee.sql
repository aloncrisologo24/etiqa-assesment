CREATE OR ALTER PROCEDURE dbo.InsertNewEmployee
--PARAMETERS--
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

	--Create employee number--
	DECLARE @EmployeeNumber VARCHAR(20);
	DECLARE @min INT = 0;
	DECLARE @max INT = 99999;
	DECLARE @randomNumber VARCHAR(5);
	SET @randomNumber = FORMAT((FLOOR(RAND() * (@max - @min + 1)) + @min), '00000');
	SET @EmployeeNumber = UPPER(
								CONCAT(
									LEFT(@LastName, 3) + REPLICATE('*', 3 - LEN(LEFT(@LastName, 3))), '-'
									, @randomNumber, '-'
									, FORMAT(@DateOfBirth, 'ddMMMyyyy')
								)
							)
	SELECT @EmployeeNumber
	
	--Insert new employee--
	INSERT INTO Employees (EmployeeNumber, FirstName, LastName, DateOfBirth, WorkingDays, DailyRate, StartDate, EndDate, TakeHomePay)
	VALUES (@EmployeeNumber, @FirstName, @LastName, @DateOfBirth, @WorkingDays, @DailyRate, @StartDate, @EndDate, @TakeHomePay)

END;
GO