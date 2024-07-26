using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using TeamManagement.Domain.Employee;
using TeamManagement.Domain.Repositories;
using TeamManagement.Infrastructure.Data;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Persistence.PostgresPersistence
{
    public class EmployeeRepositoryPostgres : IEmployeeRepository
    {
        private readonly TeamManagementDbContext _dbContext;

        public EmployeeRepositoryPostgres(TeamManagementDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeDataModel> CreateEmployee(Employee employee)
        {
            EmployeeDataModel employeeDataModel = new(employee);
            using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                EntityEntry<EmployeeDataModel> saved = _dbContext.Add(employeeDataModel);

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return saved.Entity;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                throw new Exception($"There was a problem adding: {ex}");
            }
        }

        public async Task<List<EmployeeDataModel>> GetAllEmployees()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<EmployeeDataModel?> GetEmployeeById(int employeeId)
        {
            return await _dbContext.Employees.Where(empl => empl.EmployeeId == employeeId).FirstOrDefaultAsync(); 
        }
    }
}
