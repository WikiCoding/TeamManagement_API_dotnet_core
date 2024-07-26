using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.Persistence.DataModels;
using TeamManagement.ValueObjects;

namespace TeamManagement.Services.ProjectServices
{
    public class DeleteProjectService : IDeleteProject
    {
        private readonly IProjectFactory _projectFactory;
        private readonly IProjectRepository _projectRepository;
        public DeleteProjectService(IProjectFactory projectFactory, IProjectRepository projectRepository)
        {
            _projectFactory = projectFactory;
            _projectRepository = projectRepository;
        }

        public async Task<Project> DeleteProject(int projectId)
        {
            ProjectDataModel? projectDataModel = await _projectRepository.GetProjectById(projectId);
            if (projectDataModel == null)
            {
                throw new Exception("Project doesn't exist");
            }

            ProjectDataModel deleted = await _projectRepository.DeleteProject(projectDataModel);

            ProjectId projectIdObj = new(deleted.ProjectId);
            ProjectDescription projectDescription = new(deleted.ProjectDescription);
            EmployeeId employeeId = new(deleted.EmployeeId);

            return _projectFactory.CreateProject(projectIdObj, projectDescription, employeeId);
        }
    }
}
