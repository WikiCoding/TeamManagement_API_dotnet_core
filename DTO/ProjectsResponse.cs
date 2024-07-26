namespace TeamManagement.DTO
{
    public class ProjectsResponse(int id, string projectDescription, int employeeId)
    {
        public int projectId { get; } = id;
        public string projectDescription { get; } = projectDescription;
        public int employeeId { get; } = employeeId;
    }
}
