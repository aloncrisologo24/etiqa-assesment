using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EtiqaAssessment.Models;
using EtiqaAssessment.Services;
using Microsoft.IdentityModel.Tokens;

namespace EtiqaAssessment.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public ActionResult<List<Employee>> GetAllEmployee()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            employees = _employeeService.RetrieveAllEmployees();
            return Ok(employees);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    public ActionResult<Employee> AddNewEmployee(Employee employeeData)
    {
        Employee employee = new Employee();
        try
        {
            string error = ValidateEmployeeData(employeeData);
            if(!error.IsNullOrEmpty())
            {
                return BadRequest(error);
            }
            employee = _employeeService.AddNewEmployee(employeeData);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public ActionResult<Employee> UpdateEmployee(Employee employeeData)
    {
        Employee employee = new Employee();
        try
        {
            string error = ValidateEmployeeData(employeeData);
            if (!error.IsNullOrEmpty())
            {
                return BadRequest(error);
            }
            employee = _employeeService.UpdateEmployee(employeeData);
            return Ok(employee);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{employeeNumber}")]
    public ActionResult<Employee> DeleteEmployee(string employeeNumber)
    {
        Employee employee = new Employee();
        try
        {
            _employeeService.DeleteEmployee(employeeNumber);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private string ValidateEmployeeData(Employee employeeData)
    {
        if (employeeData == null)
            return "Employee payload is required.";

        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(employeeData.FirstName))
            errors.Add("FirstName is required.");

        if (string.IsNullOrWhiteSpace(employeeData.LastName))
            errors.Add("LastName is required.");

        if (employeeData.DateOfBirth == default)
            errors.Add("DateOfBirth is required.");
        else
        {
            var today = DateTime.UtcNow.Date;
            if (employeeData.DateOfBirth > today)
                errors.Add("DateOfBirth cannot be in the future.");
            else
            {
                var age = today.Year - employeeData.DateOfBirth.Year;
                if (employeeData.DateOfBirth.Date > today.AddYears(-age)) age--;
                if (age < 18)
                    errors.Add("Employee must be at least 18 years old.");
            }
        }

        if (string.IsNullOrWhiteSpace(employeeData.WorkingDays))
            errors.Add("WorkingDays is required.");
        else if (employeeData.WorkingDays.Length > 5)
            errors.Add("WorkingDays exceeds maximum length of 5 characters.");
        else if (employeeData.WorkingDays != "MWF" && employeeData.WorkingDays != "TTHS")
            errors.Add("Invalid WorkingDays.");

        if (employeeData.DailyRate <= 0)
            errors.Add("DailyRate must be greater than 0.");

        if (employeeData.StartDate != default && employeeData.EndDate != default)
        {
            if (employeeData.EndDate <= employeeData.StartDate)
                errors.Add("EndDate must be after StartDate.");
        }

        return errors.Count == 0 ? string.Empty : string.Join("; ", errors);
    }
}
