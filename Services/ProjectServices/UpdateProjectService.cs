using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.Persistence.DataModels;
using TeamManagement.ValueObjects;

namespace TeamManagement.Services.ProjectServices
{
    public class UpdateProjectService : IUpdateProject
    {
        private readonly IProjectFactory _projectFactory;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public UpdateProjectService(IProjectFactory projectFactory, IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        {
            _projectFactory = projectFactory;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<int> UpdateProject(ProjectId projectId, ProjectDescription projectDescription, EmployeeId employeeId)
        {
            EmployeeDataModel? employeeDataModel = await _employeeRepository.GetEmployeeById(employeeId.id);

            if (employeeDataModel == null)
            {
                throw new Exception("Employee not found");
            }

            Project project = _projectFactory.CreateProject(projectId, projectDescription, employeeId);

            return await _projectRepository.UpdateProject(project);
        }
    }
}
