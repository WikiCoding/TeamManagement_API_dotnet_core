using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.Project
{
    public interface IProjectFactory
    {
        Project CreateProject(ProjectId projectId, ProjectDescription projectDescription, EmployeeId employeeId);
    }
}
