using TeamManagement.Domain.ddd;
using TeamManagement.ValueObjects;

namespace TeamManagement.Domain.Project
{
    public class Project : IAggregateRoot
    {
        public ProjectId projectId { get; set; }
        public ProjectDescription projectDescription { get; set; }
        public EmployeeId employeeId { get; set; }

        public Project(ProjectId projectId, ProjectDescription projectDescription, EmployeeId employeeId)
        {
            this.projectId = projectId;
            this.projectDescription = projectDescription;
            this.employeeId = employeeId;
        }
    }
}
