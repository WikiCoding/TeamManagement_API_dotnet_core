using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Domain.UseCases
{
    public interface IDeleteProject
    {
        Task<Domain.Project.Project> DeleteProject(int projectId);
    }
}
