using Microsoft.AspNetCore.Mvc;
using TeamManagement.Domain.Employee;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.ValueObjects;

namespace TeamManagement.Controllers.EmployeeController
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IGetAllEmployees _getAllEmployeesService;
        private readonly IAddEmployee _addEmployeeService;

        public EmployeesController(IGetAllEmployees getAllEmployees, IAddEmployee addEmployee)
        {
            _getAllEmployeesService = getAllEmployees;
            _addEmployeeService = addEmployee;
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

        private static EmployeesResponse MapEmployeeToResponse(Employee employee)
        {
            return new EmployeesResponse(employee.employeeId.id, employee.employeeName.Name, employee.role.ToString());
        }
    }
}
