using TeamManagement.Domain.Employee;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.ValueObjects;

namespace TeamManagement.Services.EmployeeServices
{
    public class UpdateEmployeeService : IUpdateEmployee
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeService(IEmployeeFactory employeeFactory, IEmployeeRepository employeeRepository)
        {
            _employeeFactory = employeeFactory;
            _employeeRepository = employeeRepository;
        }

        public async Task<int> UpdateEmployee(EmployeeId employeeId, EmployeeName employeeName, Role role)
        {
            Employee employee = _employeeFactory.CreateEmployee(employeeId, employeeName, role);

            return await _employeeRepository.UpdateEmployee(employee);
        }
    }
}
