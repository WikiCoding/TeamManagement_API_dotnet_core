using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Persistence.MemoryPersistence
{
    public class ProjectRepositoryMemory : IProjectRepository
    {
        private static int currentId = 0;
        private static readonly Dictionary<int, ProjectDataModel> _projectDb = [];

        public async Task<ProjectDataModel> CreateProject(Project project)
        {
            currentId++;
            ProjectDataModel projectDataModel = new(project);
            projectDataModel.ProjectId = currentId;

            _projectDb.Add(currentId, projectDataModel);

            // commit the added data to the database closing the transaction or handling rollback logic

            return projectDataModel;
        }

        public async Task<List<ProjectDataModel>> GetAllProjects()
        {
            return _projectDb.Values.ToList();
        }

        public async Task<ProjectDataModel?> GetProjectFromEmployee(int employeeId)
        {
            return _projectDb[employeeId] ?? null;
        }
    }
}
