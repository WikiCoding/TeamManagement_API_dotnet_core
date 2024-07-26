using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.Project
{
    public class ProjectFactoryImpl : IProjectFactory
    {
        public Project CreateProject(ProjectId projectId, ProjectDescription projectDescription, EmployeeId employeeId)
        {
            return new Project(projectId, projectDescription, employeeId);
        }
    }
}
