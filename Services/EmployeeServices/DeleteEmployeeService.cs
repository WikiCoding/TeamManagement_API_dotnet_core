using Microsoft.EntityFrameworkCore;
using TeamManagement.Domain.Employee;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.Persistence.DataModels;
using TeamManagement.ValueObjects;

namespace TeamManagement.Services.EmployeeServices
{
    public class DeleteEmployeeService : IDeleteEmployee
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IEmployeeRepository _employeeRepository;
        public DeleteEmployeeService(IEmployeeFactory employeeFactory, IEmployeeRepository employeeRepository)
        {
            _employeeFactory = employeeFactory;
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> DeleteEmployee(int employeeId)
        {
            EmployeeDataModel? employeeDataModel = await _employeeRepository.GetEmployeeById(employeeId);

            if (employeeDataModel == null)
            {
                throw new Exception("Employee doesn't exist");
            }

            // using this result for testing purposes, otherwise could be a void method
            EmployeeDataModel deleted = await _employeeRepository.DeleteEmployee(employeeDataModel);

            EmployeeId employeeIdObj = new(deleted.EmployeeId);
            EmployeeName employeeName = new(deleted.EmployeeName);
            Role role = deleted.Role;

            return _employeeFactory.CreateEmployee(employeeIdObj, employeeName, role);
        }
    }
}
