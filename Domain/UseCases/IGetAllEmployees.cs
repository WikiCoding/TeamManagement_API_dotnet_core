namespace TeamManagement.Domain.UseCases
{
    public interface IGetAllEmployees
    {
        Task<List<Domain.Employee.Employee>> GetAllEmployees();
    }
}
