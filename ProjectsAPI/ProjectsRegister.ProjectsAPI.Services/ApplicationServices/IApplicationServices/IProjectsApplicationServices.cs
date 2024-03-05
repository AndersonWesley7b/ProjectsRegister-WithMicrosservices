using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;
public interface IProjectsApplicationServices
{
    Task<List<ResumedProjectDTO>> GetAllProjects();

    Task CreateNewProject(CreateProjectDTO _NewProject, bool _Commit);
}
