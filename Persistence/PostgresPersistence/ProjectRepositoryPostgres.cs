using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
            projectDataModel.ProjectId = 0;
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

        public async Task<ProjectDataModel> DeleteProject(ProjectDataModel projectDataModel)
        {
            using IDbContextTransaction transaction = _dbContext.Database.BeginTransaction();

            try
            {
                _dbContext.Projects.Remove(projectDataModel);
                await _dbContext.SaveChangesAsync();
                transaction.Commit();

                return projectDataModel;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception($"Problem deleting project: {ex}");
            }
        }

        public async Task<List<ProjectDataModel>> GetAllProjects()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<ProjectDataModel?> GetProjectById(int projectId)
        {
            return await _dbContext.Projects.Where(proj => proj.ProjectId == projectId).FirstOrDefaultAsync();
        }

        public async Task<List<ProjectDataModel>> GetProjectsFromEmployee(int employeeId)
        {
            return await _dbContext.Projects.Where(proj => proj.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<int> UpdateProject(Project project)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                int rowsAffected = await _dbContext.Projects.Where(proj => proj.ProjectId == project.projectId.id)
                .ExecuteUpdateAsync(updates =>
                        updates.SetProperty(proj => proj.ProjectDescription, project.projectDescription.Description)
                               .SetProperty(proj => proj.EmployeeId, project.employeeId.id)
                               .SetProperty(proj => proj.Version, proj => proj.Version + 1)
                );

                if (rowsAffected == 0)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Error updating...");
                }

                await transaction.CommitAsync();

                return rowsAffected;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Concurrency error occurred while updating the project.", ex);
            }
        }
    }
}
