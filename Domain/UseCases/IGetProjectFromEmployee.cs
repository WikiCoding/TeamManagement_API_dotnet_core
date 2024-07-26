using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Domain.UseCases
{
    public interface IGetProjectFromEmployee
    {
        Task<Domain.Project.Project> GetProjectFromEmployee(int employeeId);
    }
}
