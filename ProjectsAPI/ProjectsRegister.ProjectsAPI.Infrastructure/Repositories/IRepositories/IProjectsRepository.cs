using ProjectsRegister.ProjectsAPI.Domain.Entities;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
public interface IProjectsRepository
{
    IQueryable<Project?> GetAllProjectsReadOnly();

    IQueryable<Project?> GetAllProjects();

    Task<Project?> GetProjectById(Guid _Id);

    Task<Project?> GetProjectByIdReadOnly(Guid _Id);

    Task AddProject(Project _Project);

    void DeleteProject(Project _Project);

    Task CommitChanges();
}
