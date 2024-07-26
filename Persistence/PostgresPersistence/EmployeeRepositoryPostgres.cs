﻿using Microsoft.EntityFrameworkCore;
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
            employeeDataModel.EmployeeId = 0;
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
        public async Task<EmployeeDataModel> DeleteEmployee(EmployeeDataModel employeeDataModel)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                _dbContext.Employees.Remove(employeeDataModel);

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                return employeeDataModel;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"There was a problem deleting the employee: {ex}");
            }


        }

        public async Task<int> UpdateEmployee(Employee employee)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                int rowsAffected = await _dbContext.Employees.Where(empl => empl.EmployeeId == employee.employeeId.id)
                .ExecuteUpdateAsync(updates =>
                        updates.SetProperty(empl => empl.EmployeeName, employee.employeeName.Name)
                               .SetProperty(empl => empl.Role, employee.role)
                               .SetProperty(empl => empl.Version, empl => empl.Version + 1)
                );

                if (rowsAffected == 0)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error updating...");
                }

                await transaction.CommitAsync();

                return rowsAffected;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Concurrency error occurred while updating the employee.", ex);
            }
        }
    }
}
