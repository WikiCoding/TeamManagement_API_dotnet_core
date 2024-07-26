using TeamManagement.Domain.Employee;
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
            return [.. _projectDb.Values];
        }

        public async Task<List<ProjectDataModel>> GetProjectsFromEmployee(int employeeId)
        {
            return _projectDb.Where(item => item.Value.EmployeeId == employeeId).Select(proj => proj.Value).ToList();
        }

        public async Task<ProjectDataModel?> GetProjectById(int projectId)
        {
            return _projectDb[projectId];
        }

        public async Task<ProjectDataModel> DeleteProject(ProjectDataModel projectDataModel)
        {
            _projectDb.Remove(projectDataModel.ProjectId);

            return projectDataModel;
        }

        public async Task<int> UpdateProject(Project project)
        {
            ProjectDataModel projectDataModel = new(project);
            _projectDb[project.projectId.id] = projectDataModel;
            return 1;
        }
    }
}
