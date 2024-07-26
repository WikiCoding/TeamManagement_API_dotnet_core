namespace TeamManagement.Domain.UseCases
{
    public interface IGetAllProjects
    {
        Task<List<Domain.Project.Project>> GetAllProjects();
    }
}
