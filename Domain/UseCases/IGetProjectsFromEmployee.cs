using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Domain.UseCases
{
    public interface IGetProjectsFromEmployee
    {
        Task<List<Domain.Project.Project>> GetProjectsFromEmployee(int employeeId);
    }
}
