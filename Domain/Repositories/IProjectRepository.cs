using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<ProjectDataModel> CreateProject(Domain.Project.Project project);
        Task<List<ProjectDataModel>> GetAllProjects();
        Task<List<ProjectDataModel>> GetProjectsFromEmployee(int employeeId);
        Task<ProjectDataModel?> GetProjectById(int projectId);
        Task<ProjectDataModel> DeleteProject(ProjectDataModel project);
        Task<int> UpdateProject(Domain.Project.Project project);
    }
}
