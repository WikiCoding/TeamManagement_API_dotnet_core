using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.Persistence.DataModels;
using TeamManagement.ValueObjects;

namespace TeamManagement.Services.ProjectServices
{
    public class GetAllProjectsService : IGetAllProjects
    {
        private readonly IProjectFactory _projectFactory;
        private readonly IProjectRepository _projectRepository;

        public GetAllProjectsService(IProjectFactory projectFactory, IProjectRepository projectRepository)
        {
            _projectFactory = projectFactory;
            _projectRepository = projectRepository;
        }

        public async Task<List<Project>> GetAllProjects()
        {
            List<ProjectDataModel> projectDms = await _projectRepository.GetAllProjects();

            List<Project> projects = new ProjectsMapper(_projectFactory).DataModelsToListDomain(projectDms);

            return projects;
        }
    }
}
