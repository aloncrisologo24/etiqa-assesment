namespace EtiqaAssessment.Models;

public class Employee
{
    public string? EmployeeNumber { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string WorkingDays { get; set; }

    public decimal DailyRate { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TakeHomePay { get; set; }
}
