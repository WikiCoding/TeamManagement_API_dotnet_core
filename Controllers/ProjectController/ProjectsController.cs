using Microsoft.AspNetCore.Mvc;
using TeamManagement.Domain.UseCases;
using TeamManagement.DTO;
using TeamManagement.ValueObjects;
using TeamManagement.Domain.Project;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TeamManagement.Controllers.ProjectController
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IAddProject _addProjectService;
        private readonly IGetAllProjects _getAllProjects;
        private readonly IGetProjectFromEmployee _getProjectFromEmployee;

        public ProjectsController(IAddProject addProjectService, IGetAllProjects getAllProjects, IGetProjectFromEmployee getProjectFromEmployee)
        {
            _addProjectService = addProjectService;
            _getAllProjects = getAllProjects;
            _getProjectFromEmployee = getProjectFromEmployee;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectsResponse>>> GetAll()
        {
            List<Project> projects = await _getAllProjects.GetAllProjects();

            List<ProjectsResponse> response = ProjectsMapper.ProjectsToProjectsResponse(projects);

            return Ok(response);
        }

        [HttpGet("employee/{employee-id}")]
        public async Task<IActionResult> GetProjectFromEmployee([FromRoute(Name = "employee-id")] int employeeId)
        {
            Project project;
            try
            {
                project = await _getProjectFromEmployee.GetProjectFromEmployee(employeeId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            ProjectsResponse projectsResponse = new(project.projectId.id, project.projectDescription.Description, project.employeeId.id);

            return Ok(projectsResponse);
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
    }
}
