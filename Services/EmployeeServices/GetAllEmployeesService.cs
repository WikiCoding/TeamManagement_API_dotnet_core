using TeamManagement.Domain.Employee;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Services.EmployeeServices
{
    public class GetAllEmployeesService : IGetAllEmployees
    {
        private readonly IEmployeeFactory _employeeFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllEmployeesService(IEmployeeFactory employeeFactory, IEmployeeRepository employeeRepository)
        {
            _employeeFactory = employeeFactory;
            _employeeRepository = employeeRepository;
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            List<EmployeeDataModel> employeeDataModels =  await _employeeRepository.GetAllEmployees();

            return new EmployeesMapper(_employeeFactory).DataModelsToListEmployee(employeeDataModels);
        }
    }
}
