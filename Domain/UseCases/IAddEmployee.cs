using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.UseCases
{
    public interface IAddEmployee
    {
        Task<Domain.Employee.Employee> CreateEmployee(EmployeeId employeeId, EmployeeName employeeName, Role role);
    }
}
