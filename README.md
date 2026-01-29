# Etiqa Assessment

**Description**

This project is made with ASP.NET Core backend (REST API) and an Angular frontend. The backend provides employee management APIs and business logic to calculate employee take-home pay over a date range based on birthdays and working days. The project uses EF Core with stored procedures for database operations.

---

## Backend: Function Summary

### EmployeeController (API)
- `GET /api/employees` — Fetch all non-archived employees
- `POST /api/employees` — Create a new employee (validates payload)
- `PUT /api/employees` — Update an existing employee (validates payload)
- `DELETE /api/employees/{employeeNumber}` — Soft-delete (archive)

Note: Validation ensures required fields, age >= 18, correct `WorkingDays` (`MWF` or `TTHS`), non-zero `DailyRate`, and valid date ranges.

### EmployeeService
- `RetrieveAllEmployees()` — returns all employees
- `AddNewEmployee(Employee)` — computes `TakeHomePay` then inserts
- `UpdateEmployee(Employee)` — recomputes `TakeHomePay` then updates
- `DeleteEmployee(string)` — deletes (archives) employee
- Private: `CalculateTakeHomePay(Employee)` — private method that computes the `TakeHomePay`
  - Counts birthdays between `StartDate` and `EndDate` (handles Feb 29).
  - Counts working days based on `WorkingDays` (`MWF` -> Mon/Wed/Fri; `TTHS` -> Tue/Thu/Sat).
  - Formula: `TakeHomePay = (DailyRate * birthdayCount) + (DailyRate * 2 * WorkDayCount)` (birthdays: 100% of daily; work days: 200%).
    
    Note: If birthday falls in the same day as work day, it is additive.

### EmployeeRepository
- `GetAllEmployees()` — uses `dbo.GetAllEmployees` stored procedure
- `InsertNewEmployee(Employee)` — uses `dbo.InsertNewEmployee` stored procedure
- `UpdateEmployee(Employee)` — uses `dbo.UpdateEmployee` stored procedure
- `DeleteEmployee(string)` — uses `dbo.DeleteEmployee` stored procedure (sets `IsArchived = 1`)

DB scripts are under `backend/EtiqaAssessment/DB/`:
- `CreateEmployeesTable.sql` — table schema
- `GetAllEmployees.sql`, `InsertNewEmployee.sql`, `UpdateEmployee.sql`, `DeleteEmployee.sql`

  Note: EmployeeNumber is auto-generated in the `InsertNewEmployee.sql` stored procedure.
---

## Additional Info & Previews

- The frontend is a separate Angular app under `frontend/etiqa-frontend` and talks to the `api/employees` endpoints.
<img width="500" alt="image" src="https://github.com/user-attachments/assets/25c3bb6f-3d32-4169-8089-21e08de5441d" />
<img width="500" alt="image" src="https://github.com/user-attachments/assets/9d0ac61f-0346-4d81-847c-019d19f0146b" />

<img width="838" height="64" alt="image" src="https://github.com/user-attachments/assets/9274a6cd-72fd-4a9b-a28c-347e10487663" />


---

> _This README summarizes project responsibilities and how the backend is structured. The codes contains more detailed inline comments._

---
