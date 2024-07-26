using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.UseCases
{
    public interface IUpdateEmployee
    {
        Task<int> UpdateEmployee(EmployeeId employeeId, EmployeeName employeeName, Role role);
    }
}
