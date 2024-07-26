using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Services.ProjectServices
{
    public class GetProjectsFromEmployeeService : IGetProjectsFromEmployee
    {
        private readonly IProjectFactory _projectFactory;
        private readonly IProjectRepository _projectRepository;

        public GetProjectsFromEmployeeService(IProjectFactory projectFactory, IProjectRepository projectRepository)
        {
            _projectFactory = projectFactory;
            _projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetProjectsFromEmployee(int employeeId)
        {
            List<ProjectDataModel> projectDataModels = await _projectRepository.GetProjectsFromEmployee(employeeId);

            if (projectDataModels == null)
            {
                throw new Exception("No projects with this employee id");
            }

            List<Project> project = new ProjectsMapper(_projectFactory).DataModelsToListDomain(projectDataModels);

            return project;
        }
    }
}
