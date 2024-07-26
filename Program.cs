using Microsoft.EntityFrameworkCore;
using TeamManagement.Domain.Employee;
using TeamManagement.Domain.Project;
using TeamManagement.Domain.Repositories;
using TeamManagement.Domain.UseCases;
using TeamManagement.Infrastructure.Data;
using TeamManagement.Persistence.MemoryPersistence;
using TeamManagement.Persistence.PostgresPersistence;
using TeamManagement.Services.EmployeeServices;
using TeamManagement.Services.ProjectServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IProjectFactory, ProjectFactoryImpl>();
builder.Services.AddScoped<IEmployeeFactory, EmployeeFactoryImpl>();
//builder.Services.AddScoped<IProjectRepository, ProjectRepositoryMemory>();
//builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositoryMemory>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepositoryPostgres>();
builder.Services.AddScoped<IProjectRepository, ProjectRepositoryPostgres>();
builder.Services.AddScoped<IAddProject, AddProjectService>();
builder.Services.AddScoped<IGetAllProjects, GetAllProjectsService>();
builder.Services.AddScoped<IGetProjectsFromEmployee, GetProjectsFromEmployeeService>();
builder.Services.AddScoped<IUpdateProject, UpdateProjectService>();
builder.Services.AddScoped<IDeleteProject, DeleteProjectService>();
builder.Services.AddScoped<IAddEmployee, AddEmployeeService>();
builder.Services.AddScoped<IGetAllEmployees, GetAllEmployeesService>();
builder.Services.AddScoped<IUpdateEmployee, UpdateEmployeeService>();
builder.Services.AddScoped<IDeleteEmployee, DeleteEmployeeService>();
builder.Services.AddDbContext<TeamManagementDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("TeamManagementDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
