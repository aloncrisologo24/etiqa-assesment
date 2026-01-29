using EtiqaAssessment.DB;
using EtiqaAssessment.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EtiqaAssessment.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly ApplicationDbContext _dbcontext;
        public EmployeeRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<Employee> GetAllEmployees()
        {
            var result = _dbcontext.Employees
                .FromSqlInterpolated($"EXEC dbo.GetAllEmployees")
                .ToList();


            return result
                .Select(r => new Employee
                {
                    EmployeeNumber = r.EmployeeNumber,
                    FirstName = r.FirstName,
                    LastName = r.LastName,
                    DateOfBirth = r.DateOfBirth,
                    WorkingDays = r.WorkingDays,
                    DailyRate = r.DailyRate,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TakeHomePay = r.TakeHomePay
                }).ToList();
        }

        public Employee InsertNewEmployee(Employee employeeData)
        {
            _dbcontext.Database.ExecuteSqlInterpolated($@"EXEC dbo.InsertNewEmployee 
                @FirstName={employeeData.FirstName}, 
                @LastName={employeeData.LastName}, 
                @DateOfBirth={employeeData.DateOfBirth}, 
                @WorkingDays={employeeData.WorkingDays}, 
                @DailyRate={employeeData.DailyRate},
                @StartDate={employeeData.StartDate},
                @EndDate={employeeData.EndDate},
                @TakeHomePay={employeeData.TakeHomePay}
                ");

            return employeeData;
        }

        public Employee UpdateEmployee(Employee employeeData)
        {
            _dbcontext.Database.ExecuteSqlInterpolated($@"EXEC dbo.UpdateEmployee 
                @EmployeeNumber={employeeData.EmployeeNumber},
                @FirstName={employeeData.FirstName}, 
                @LastName={employeeData.LastName}, 
                @DateOfBirth={employeeData.DateOfBirth}, 
                @WorkingDays={employeeData.WorkingDays}, 
                @DailyRate={employeeData.DailyRate},
                @StartDate={employeeData.StartDate},
                @EndDate={employeeData.EndDate},
                @TakeHomePay={employeeData.TakeHomePay}
                ");

            return employeeData;
        }

        public void DeleteEmployee(string employeeNumber)
        {
            _dbcontext.Database.ExecuteSqlInterpolated($@"EXEC dbo.DeleteEmployee 
                @EmployeeNumber={employeeNumber}");
        }
    }
}