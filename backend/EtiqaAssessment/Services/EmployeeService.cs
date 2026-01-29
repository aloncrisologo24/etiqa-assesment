using EtiqaAssessment.Repository;
using EtiqaAssessment.Models;
using System.Reflection.Metadata.Ecma335;

namespace EtiqaAssessment.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> RetrieveAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public Employee AddNewEmployee(Employee employeeData)
        {
            employeeData.TakeHomePay = CalculateTakeHomePay(employeeData);
            return _employeeRepository.InsertNewEmployee(employeeData);
        }

        public Employee UpdateEmployee(Employee employeeData)
        {
            employeeData.TakeHomePay = CalculateTakeHomePay(employeeData);
            return _employeeRepository.UpdateEmployee(employeeData);
        }

        public void DeleteEmployee(string employeeNumber)
        {
            _employeeRepository.DeleteEmployee(employeeNumber);
        }

        private static decimal CalculateTakeHomePay(Employee employeeData)
        {
            decimal TakeHomePay = 0;


            // Counting birthdays in the date range
            int birthdayCount = 0;

            // Get Year range
            int StartYear = employeeData.StartDate.Year;
            int EndYear = employeeData.EndDate.Year;

            // Original date of birth for month and day
            DateTime origDoB = employeeData.DateOfBirth;

            // For leap year checking
            bool isLeapYearDayBaby = false;
            if ((employeeData.DateOfBirth.Month == 2 && employeeData.DateOfBirth.Day == 29))
            {
                isLeapYearDayBaby = true;
            }

            // Start looping through the year range
            for (int i = StartYear; i <= EndYear; i++)
            {
                // Temporary date of birth variable for checking
                DateTime tempDoB = new DateTime();

                // This If-block will change the birthday year to i for every iteration

                if (isLeapYearDayBaby && !DateTime.IsLeapYear(i)) // If the employee is born on Feb 29, check for leap year
                {
                    tempDoB = new DateTime(i, 3, 1); // Set to March 1 if it is not a leap year
                }
                else
                {
                    tempDoB = new DateTime(i, origDoB.Month, origDoB.Day);
                }

                // If the birthday is within the date range, increase birthday count
                if (tempDoB >= employeeData.StartDate && tempDoB <= employeeData.EndDate)
                {
                    birthdayCount++;
                }
            }


            // Counting work days in the date range

            TimeSpan ts = employeeData.EndDate.AddDays(1) - employeeData.StartDate;     // Total duration; Add 1 day to include end date
            int WeekCount = (int)Math.Floor(ts.TotalDays / 7);                          // Number of whole weeks
            int Remainder = (int)(ts.TotalDays % 7);                                    // Number of remaining days

            // Initialize WorkDayCount setting 3 days for each whole week within the date range
            int WorkDayCount = WeekCount * 3;

            // Set working days
            DayOfWeek[] WorkingDays =
                employeeData.WorkingDays == "MWF"
                    ? new[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }
                    : new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Saturday };

            // Start looping through the remaining days
            for (int i = Remainder; i > 0; i--)
            {

                // Get the day of week of the remaining day 
                DayOfWeek tempDoW = employeeData.EndDate.AddDays(-(i - 1)).DayOfWeek;

                if (WorkingDays.Contains(tempDoW)){
                    // Add 1 to WorkDayCount if the day is a working day
                    WorkDayCount++;
                }
            }

            // Calculate TakeHomePay
            TakeHomePay = (employeeData.DailyRate * birthdayCount)  // Employees get 100% of daily rate on birthdays
                            + (employeeData.DailyRate * 2 * WorkDayCount); // Employees get 200% daily rate on work days


            return TakeHomePay;
        }
    }
}