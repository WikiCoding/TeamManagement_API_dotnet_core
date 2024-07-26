using System.ComponentModel.DataAnnotations;
using TeamManagement.Domain.Employee;
using TeamManagement.ValueObjects;

namespace TeamManagement.Persistence.DataModels
{
    public class EmployeeDataModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public Role Role { get; set; }

        [ConcurrencyCheck]
        public int Version { get; set; }

        public EmployeeDataModel()
        {
            
        }

        public EmployeeDataModel(Employee employee)
        {
            EmployeeId = employee.employeeId.id;
            EmployeeName = employee.employeeName.Name;
            Role = employee.role;
        }
    }
}
