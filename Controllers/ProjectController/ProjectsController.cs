using Microsoft.AspNetCore.Mvc;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.ValueObjects;
using TeamManagement.Domain.Project;

namespace TeamManagement.Controllers.ProjectController
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IAddProject _addProjectService;
        private readonly IGetAllProjects _getAllProjects;
        private readonly IGetProjectsFromEmployee _getProjectFromEmployee;
        private readonly IDeleteProject _deleteProject;

        public ProjectsController(IAddProject addProjectService, IGetAllProjects getAllProjects, IGetProjectsFromEmployee getProjectFromEmployee, IDeleteProject deleteProject)
        {
            _addProjectService = addProjectService;
            _getAllProjects = getAllProjects;
            _getProjectFromEmployee = getProjectFromEmployee;
            _deleteProject = deleteProject;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectsResponse>>> GetAll()
        {
            List<Project> projects = await _getAllProjects.GetAllProjects();

            List<ProjectsResponse> response = ProjectsMapper.ProjectsToProjectsResponse(projects);

            return Ok(response);
        }

        [HttpGet("employee/{employee-id}")]
        public async Task<ActionResult<List<ProjectsResponse>>> GetProjectFromEmployee([FromRoute(Name = "employee-id")] int employeeId)
        {
            List<Project> projects;
            try
            {
                projects = await _getProjectFromEmployee.GetProjectsFromEmployee(employeeId);

                List<ProjectsResponse> response = projects.ConvertAll<ProjectsResponse>(proj => MapProjectToResponse(proj)).ToList();

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(AddProjectRequest request)
        {
            ProjectId projectId = new(1);
            ProjectDescription projectDescription;
            EmployeeId employeeId;
            try
            {
                projectDescription = new ProjectDescription(request.projectDescription);
                employeeId = new EmployeeId(request.employeeId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                Project project = await _addProjectService.CreateProject(projectId, projectDescription, employeeId);

                ProjectsResponse projectsResponse = new ProjectsResponse(project.projectId.id, project.projectDescription.Description, project.employeeId.id);

                return CreatedAtAction(nameof(AddProject), projectsResponse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{project-id}")]
        public async Task<IActionResult> DeleteProject([FromRoute(Name = "project-id")] int projectId)
        {
            try
            {
                Project project = await _deleteProject.DeleteProject(projectId);

                return Ok(project);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static ProjectsResponse MapProjectToResponse(Project project)
        {
            return new ProjectsResponse(project.projectId.id, project.projectDescription.Description, project.employeeId.id);
        }
    }
}
