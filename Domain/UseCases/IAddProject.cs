using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.UseCases
{
    public interface IAddProject
    {
        Task<Domain.Project.Project> CreateProject(ProjectId projectId, ProjectDescription projectDescription, EmployeeId employeeId);
    }
}
