using TeamManagement.Domain.Employee;
using TeamManagement.Persistence.DataModels;
using TeamManagement.ValueObjects;

namespace TeamManagement.DTO
{
    public class EmployeesMapper
    {
        private readonly IEmployeeFactory _employeeFactory;

        public EmployeesMapper(IEmployeeFactory employeeFactory)
        {
            _employeeFactory = employeeFactory;
        }
        public Employee DataModelToDomain(EmployeeDataModel employeeDataModel)
        {
            EmployeeId employeeId = new(employeeDataModel.EmployeeId);
            EmployeeName employeeName = new(employeeDataModel.EmployeeName);
            Role role = employeeDataModel.Role;

            return _employeeFactory.CreateEmployee(employeeId, employeeName, role);
        }

        public List<Employee> DataModelsToListEmployee(List<EmployeeDataModel> employeeDataModels)
        {
            return employeeDataModels.ConvertAll<Employee>(edm => DataModelToDomain(edm));
        }
    }
}
