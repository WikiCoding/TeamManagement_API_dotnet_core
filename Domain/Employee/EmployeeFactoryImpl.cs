using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.Employee
{
    public class EmployeeFactoryImpl : IEmployeeFactory
    {
        public Employee CreateEmployee(EmployeeId employeeId, EmployeeName employeeName, Role role)
        {
            return new Employee(employeeId, employeeName, role);
        }
    }
}
