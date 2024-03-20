using ProjectsRegister.ProjectsAPI.Domain.Entities;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
public interface IProjectsRepository
{
    IQueryable<Project?> GetAllProjectsReadOnly();

    IQueryable<Project?> GetAllProjects();

    Task<Project?> GetProjectById(Guid _Id);

    Task<Project?> GetProjectByIdReadOnly(Guid _Id);

    IQueryable<Project> GetProjectsByUserId(Guid _UserId);

    IQueryable<Project> GetProjectsByUserIdReadOnly(Guid _UserId);

    Task<bool> CheckProjectExists(Guid _Id);

    Task AddProject(Project _Project);

    void DeleteProject(Project _Project);

    void DeleteProjects(List<Project> _Projects);

}
