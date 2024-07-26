using TeamManagement.Persistence.DataModels;
using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.UseCases
{
    public interface IDeleteEmployee
    {
        Task<Domain.Employee.Employee> DeleteEmployee(int employeeId);
    }
}
