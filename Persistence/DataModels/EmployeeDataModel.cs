using System.ComponentModel.DataAnnotations;
using TeamManagement.Domain.Employee;
using TeamManagement.ValueObjects;

namespace TeamManagement.Persistence.DataModels
{
    public class EmployeeDataModel(Employee employee)
    {
        [Key]
        public int EmployeeId { get; set; } = employee.employeeId.id;
        [Required]
        public string EmployeeName { get; set; } = employee.employeeName.Name;
        [Required]
        public Role Role { get; set; } = employee.role;
    }
}
