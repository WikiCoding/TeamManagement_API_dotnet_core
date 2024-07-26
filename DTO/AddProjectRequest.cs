namespace TeamManagement.DTO
{
    public class AddProjectRequest(string projectDescription, int employeeId)
    {
        public string projectDescription { get; } = projectDescription;
        public int employeeId { get; } = employeeId;
    }
}
