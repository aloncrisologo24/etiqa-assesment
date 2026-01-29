IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Employees' AND schema_id = SCHEMA_ID('dbo'))
BEGIN

CREATE TABLE EtiqaAssessmentDB.dbo.Employees (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	EmployeeNumber VARCHAR(20) UNIQUE NOT NULL,
	FirstName VARCHAR(200) NOT NULL,
	LastName VARCHAR(200) NOT NULL,
	DateOfBirth DATE NOT NULL,
	DailyRate MONEY NOT NULL,
	WorkingDays VARCHAR(5) NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TakeHomePay MONEY NOT NULL,
	IsArchived BIT DEFAULT '0'
)
END
GO