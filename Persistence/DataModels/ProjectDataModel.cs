using System.ComponentModel.DataAnnotations;
using TeamManagement.Domain.Project;

namespace TeamManagement.Persistence.DataModels
{
    public class ProjectDataModel
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        public string? ProjectDescription { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        public ProjectDataModel()
        {
            
        }

        public ProjectDataModel(Project project)
        {
            ProjectId = project.projectId.id;
            ProjectDescription = project.projectDescription.Description;
            EmployeeId = project.employeeId.id;
        }
    }
}
