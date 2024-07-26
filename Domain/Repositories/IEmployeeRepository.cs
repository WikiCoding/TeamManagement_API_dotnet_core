using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDataModel> CreateEmployee(Domain.Employee.Employee employee);
        Task<List<EmployeeDataModel>> GetAllEmployees();
        Task<EmployeeDataModel?> GetEmployeeById(int employeeId);
        Task<EmployeeDataModel> DeleteEmployee(EmployeeDataModel employee);
    }
}
