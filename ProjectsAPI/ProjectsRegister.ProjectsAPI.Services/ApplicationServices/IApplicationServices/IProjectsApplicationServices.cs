using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;
public interface IProjectsApplicationServices
{
    protected IQueryable<ResumedProjectDTO> GetAllProjectsQuery();
    Task<List<ResumedProjectDTO>> GetAllProjects();
}
