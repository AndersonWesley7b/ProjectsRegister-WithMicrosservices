using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public class ProjectsApplicationServices : IProjectsApplicationServices
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsApplicationServices(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ResumedProjectDTO?>> GetAllProjects()
    {
        List<ResumedProjectDTO> Projects = await _projectRepository.GetAllProjects().Select.ToListAsync();
    }
}
