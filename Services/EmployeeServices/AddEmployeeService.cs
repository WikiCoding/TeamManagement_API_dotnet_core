using TeamManagement.Domain.Employee;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.Persistence.DataModels;
using TeamManagement.ValueObjects;
using TeamManagement.DTO;

namespace TeamManagement.Services.EmployeeServices
{
    public class AddEmployeeService : IAddEmployee
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public AddEmployeeService(IEmployeeFactory employeeFactory, IEmployeeRepository employeeRepository)
        {
            _employeeFactory = employeeFactory;
            _employeeRepository = employeeRepository;
        }
        public async Task<Employee> CreateEmployee(EmployeeId employeeId, EmployeeName employeeName, Role role)
        {
            Employee employee = new(employeeId, employeeName, role);

            EmployeeDataModel saved = await _employeeRepository.CreateEmployee(employee);

            return new EmployeesMapper(_employeeFactory).DataModelToDomain(saved);
        }
    }
}
