using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
using ProjectsRegister.ProjectsAPI.Domain.Entities;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public sealed class ProjectsApplicationServices : IProjectsApplicationServices
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsApplicationServices(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    private IQueryable<ResumedProjectDTO> GetAllProjectsQuery()
    {
        IQueryable<Project?> queryProjects = _projectRepository.GetAllProjectsReadOnly();

        IQueryable<ResumedProjectDTO> query = (from Project in queryProjects
                                               select new ResumedProjectDTO
                                               {
                                                   ProjectId = Project.ProjectId,
                                                   Name = Project.Name,
                                                   UserId = Project.UserId,
                                               });

        return query;

    }

    public async Task<List<ResumedProjectDTO>> GetAllProjects()
    {
        List<ResumedProjectDTO> projects = await GetAllProjectsQuery().ToListAsync();
        return projects;
    }
}
