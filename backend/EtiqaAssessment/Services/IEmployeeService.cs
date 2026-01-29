using EtiqaAssessment.Models;

namespace EtiqaAssessment.Services
{
    public interface IEmployeeService
    {
        List<Employee> RetrieveAllEmployees();

        Employee AddNewEmployee(Employee employeeData);

        Employee UpdateEmployee(Employee employeeData);

        void DeleteEmployee(string employeeNumber);
    }
}