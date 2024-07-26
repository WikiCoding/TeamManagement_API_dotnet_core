using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.Employee
{
    public class Employee(EmployeeId employeeId, EmployeeName employeeName, Role role)
    {
        public EmployeeId employeeId = employeeId;
        public EmployeeName employeeName = employeeName;
        public Role role = role;
    }
}
