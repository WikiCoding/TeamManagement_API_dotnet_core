using System.ComponentModel.DataAnnotations;
using TeamManagement.Domain.Project;

namespace TeamManagement.Persistence.DataModels
{
    public class ProjectDataModel(Project project)
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string? ProjectDescription { get; set; } = project.projectDescription.Description;

        [Required]
        public int EmployeeId { get; set; } = project.employeeId.id;
    }
}
