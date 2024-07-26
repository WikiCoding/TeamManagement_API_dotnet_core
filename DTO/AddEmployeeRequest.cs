namespace TeamManagement.DTO
{
    public class AddEmployeeRequest(string employeeName, string role)
    {
        public string EmployeeName { get; } = employeeName;
        public string Role { get; } = role;
    }
}
