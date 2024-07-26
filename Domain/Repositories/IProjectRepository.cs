using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<ProjectDataModel> CreateProject(Domain.Project.Project project);
        Task<List<ProjectDataModel>> GetAllProjects();
        Task<ProjectDataModel?> GetProjectFromEmployee(int employeeId);
    }
}
