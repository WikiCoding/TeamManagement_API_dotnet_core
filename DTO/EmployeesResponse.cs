namespace TeamManagement.DTO
{
    public class EmployeesResponse(int employeeId, string employeeName, string role)
    {
        public int EmployeeId { get; } = employeeId;
        public string EmployeeName { get; } = employeeName;
        public string Role { get; } = role;
    }
}
