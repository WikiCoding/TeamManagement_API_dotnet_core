using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Services.ProjectServices
{
    public class GetProjectFromEmployeeService : IGetProjectFromEmployee
    {
        private readonly IProjectFactory _projectFactory;
        private readonly IProjectRepository _projectRepository;

        public GetProjectFromEmployeeService(IProjectFactory projectFactory, IProjectRepository projectRepository)
        {
            _projectFactory = projectFactory;
            _projectRepository = projectRepository;
        }

        public async Task<Project> GetProjectFromEmployee(int employeeId)
        {
            ProjectDataModel? projectDataModel = await _projectRepository.GetProjectFromEmployee(employeeId);

            if (projectDataModel == null)
            {
                throw new Exception("No projects with this employee id");
            }

            Project project = new ProjectsMapper(_projectFactory).DataModelToDomain(projectDataModel);

            return project;
        }
    }
}
