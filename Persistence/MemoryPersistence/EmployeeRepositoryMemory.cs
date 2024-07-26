using TeamManagement.Domain.Employee;
using TeamManagement.Domain.Repositories;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Persistence.MemoryPersistence
{
    public class EmployeeRepositoryMemory : IEmployeeRepository
    {
        private static int _employeeId = 0;
        private static readonly Dictionary<int, EmployeeDataModel> _employeeDb = [];

        public async Task<EmployeeDataModel> CreateEmployee(Employee employee)
        {
            _employeeId++;
            EmployeeDataModel employeeDataModel = new(employee);
            employeeDataModel.EmployeeId = _employeeId;

            _employeeDb.Add(_employeeId, employeeDataModel);

            // commit the added data to the database closing the transaction or handling rollback logic 

            return employeeDataModel;
        }

        public async Task<List<EmployeeDataModel>> GetAllEmployees()
        {
            return [.. _employeeDb.Values];
        }

        public async Task<EmployeeDataModel?> GetEmployeeById(int employeeId)
        {
            return _employeeDb[employeeId] ?? null;
        }

        public async Task<EmployeeDataModel> DeleteEmployee(EmployeeDataModel employeeDataModel)
        {
            _employeeDb.Remove(employeeDataModel.EmployeeId);
            
            return employeeDataModel;
        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            EmployeeDataModel employeeDataModel = new(employee);
            _employeeDb[employee.employeeId.id] = employeeDataModel;
            return 1;
        }
    }
}
