using Microsoft.EntityFrameworkCore;
using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Infrastructure.Data;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Persistence.PostgresPersistence
{
    public class ProjectRepositoryPostgres : IProjectRepository
    {
        private readonly TeamManagementDbContext _dbContext;

        public ProjectRepositoryPostgres(TeamManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectDataModel> CreateProject(Project project)
        {
            ProjectDataModel projectDataModel = new(project);
            using var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var saved = _dbContext.Add(projectDataModel);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();

                return saved.Entity;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Error saving project: {ex}");
            }
        }

        public async Task<List<ProjectDataModel>> GetAllProjects()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<ProjectDataModel?> GetProjectFromEmployee(int employeeId)
        {
            return await _dbContext.Projects.Where(proj => proj.EmployeeId == employeeId).FirstOrDefaultAsync();
        }
    }
}
