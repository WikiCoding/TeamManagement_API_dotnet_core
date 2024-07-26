using Microsoft.EntityFrameworkCore;
using TeamManagement.Persistence.DataModels;

namespace TeamManagement.Infrastructure.Data
{
    public class TeamManagementDbContext(DbContextOptions<TeamManagementDbContext> options) : DbContext(options)
    {
        public DbSet<ProjectDataModel> Projects { get; set; }
        public DbSet<EmployeeDataModel> Employees { get; set; }
    }
}
