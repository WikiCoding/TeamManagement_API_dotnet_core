using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.ValueObjects;
using TeamManagement.Persistence.DataModels;
using TeamManagement.Domain.Employee;

namespace TeamManagement.Services.ProjectServices
{
    public class AddProjectService : IAddProject
    {
        private readonly IProjectFactory _projectFactory;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AddProjectService(IProjectFactory projectFactory, IProjectRepository projectRepository, IEmployeeRepository employeeRepository) { 
            _projectFactory = projectFactory;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Project> CreateProject(ProjectId projectId, ProjectDescription projectDescription, EmployeeId employeeId)
        {
            EmployeeDataModel? employeeDataModel = await _employeeRepository.GetEmployeeById(employeeId.id);

            if (employeeDataModel == null)
            {
                throw new Exception("Employee not found or doesn't exist");
            }

            Project project = _projectFactory.CreateProject(projectId, projectDescription, employeeId);

            ProjectDataModel projectDataModel = await _projectRepository.CreateProject(project);
            
            project.projectId = new ProjectId(projectDataModel.ProjectId);

            return project;
        }
    }
}
