using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.UseCases
{
    public interface IUpdateProject
    {
        Task<int> UpdateProject(ProjectId projectId, ProjectDescription projectDescription, EmployeeId employeeId);
    }
}
