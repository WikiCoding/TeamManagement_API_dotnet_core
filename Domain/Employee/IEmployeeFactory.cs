using TeamManagement.ValueObjects;
using TeamManagement.Domain.Employee;

namespace TeamManagement.Domain.Employee
{
    public interface IEmployeeFactory
    {
        Employee CreateEmployee(EmployeeId employeeId, EmployeeName employeeName, Role role);
    }
}
