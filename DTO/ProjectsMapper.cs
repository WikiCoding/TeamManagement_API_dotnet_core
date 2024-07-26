using TeamManagement.Persistence.DataModels;
using TeamManagement.Domain.Project;
using TeamManagement.ValueObjects;
using System.Collections;

namespace TeamManagement.DTO
{
    public class ProjectsMapper
    {
        private readonly IProjectFactory _projectFactory;
        public ProjectsMapper(IProjectFactory projectFactory)
        {
            _projectFactory = projectFactory;
        }
        public Project DataModelToDomain(ProjectDataModel dataModel)
        {
            ProjectId projectId = new(dataModel.ProjectId);
            ProjectDescription projectDescription = new(dataModel.ProjectDescription);
            EmployeeId employeeId = new(dataModel.EmployeeId);

            return _projectFactory.CreateProject(projectId, projectDescription, employeeId);
        }

        public List<Project> DataModelsToListDomain(List<ProjectDataModel> dataModels)
        {
            return dataModels.ConvertAll<Project>(dm => DataModelToDomain(dm));
        }

        public static List<ProjectsResponse> ProjectsToProjectsResponse(List<Project> projects)
        {
            List<ProjectsResponse> response = [];

            foreach (var project in projects)
            {
                ProjectsResponse projectsResponse = new(project.projectId.id, project.projectDescription.Description, project.employeeId.id);
                response.Add(projectsResponse);
            }

            return response;
        }
    }
}
