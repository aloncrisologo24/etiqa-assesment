using EtiqaAssessment.Models;

namespace EtiqaAssessment.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();

        Employee InsertNewEmployee(Employee employeeData);

        Employee UpdateEmployee(Employee employeeData);
        void DeleteEmployee(string employeeNumber);
    }
}