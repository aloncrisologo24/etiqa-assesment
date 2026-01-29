
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'EtiqaAssessmentDB')
BEGIN
    CREATE DATABASE EtiqaAssessmentDB;
END;
GO