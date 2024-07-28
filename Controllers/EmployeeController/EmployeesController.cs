using Microsoft.AspNetCore.Mvc;
using TeamManagement.Domain.Employee;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.ValueObjects;

namespace TeamManagement.Controllers.EmployeeController
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IGetAllEmployees _getAllEmployeesService;
        private readonly IAddEmployee _addEmployeeService;
        private readonly IUpdateEmployee _updateEmployeeService;
        private readonly IDeleteEmployee _deleteEmployeeService;

        public EmployeesController(IGetAllEmployees getAllEmployees, IAddEmployee addEmployee, IUpdateEmployee updateEmployee, IDeleteEmployee deleteEmployeeService)
        {
            _getAllEmployeesService = getAllEmployees;
            _addEmployeeService = addEmployee;
            _updateEmployeeService = updateEmployee;
            _deleteEmployeeService = deleteEmployeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeesResponse>>> GetAllEmployees()
        {
            List<Employee> employees = await _getAllEmployeesService.GetAllEmployees();
            List<EmployeesResponse> response = employees.ConvertAll<EmployeesResponse>(empl => MapEmployeeToResponse(empl));

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest employeeRequest)
        {
            EmployeeId employeeId;
            EmployeeName employeeName;
            Role role;
            try
            {
                employeeId = new(1);
                employeeName = new(employeeRequest.EmployeeName);
                role = (Role)Enum.Parse(typeof(Role), employeeRequest.Role.Trim().ToUpperInvariant());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            Employee employee = await _addEmployeeService.CreateEmployee(employeeId, employeeName, role);

            EmployeesResponse employeesResponse = MapEmployeeToResponse(employee);

            return CreatedAtAction(nameof(AddEmployee), employeesResponse);
        }

        [HttpPut("{employee-id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute(Name = "employee-id")] int employeeId, AddEmployeeRequest request)
        {
            EmployeeId employeeIdObj;
            EmployeeName employeeName;
            Role role;
            try
            {
                employeeIdObj = new(employeeId);
                employeeName = new(request.EmployeeName);
                role = (Role)Enum.Parse(typeof(Role), request.Role.Trim().ToUpperInvariant());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                int rowsAffected = await _updateEmployeeService.UpdateEmployee(employeeIdObj, employeeName, role);
                if (rowsAffected == 0)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{employee-id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute(Name = "employee-id")] int employeeId)
        {
            try
            {
                Employee employee = await _deleteEmployeeService.DeleteEmployee(employeeId);

                EmployeesResponse employeesResponse = MapEmployeeToResponse(employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static EmployeesResponse MapEmployeeToResponse(Employee employee)
        {
            return new EmployeesResponse(employee.employeeId.id, employee.employeeName.Name, employee.role.ToString());
        }
    }
}
