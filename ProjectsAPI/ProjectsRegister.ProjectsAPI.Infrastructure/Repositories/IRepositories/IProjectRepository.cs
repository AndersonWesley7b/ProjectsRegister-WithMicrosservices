using ProjectsRegister.ProjectsAPI.Domain.Entities;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
public interface IProjectRepository
{
    IQueryable<Project?> GetAllProjectsReadOnly();

    IQueryable<Project?> GetAllProjects();

    Task<Project?> GetProjectById(Guid _Id);

    Task AddProject(Project _Project);

    Task DeleteProjectById(Guid _Id);

    Task CommitChanges();
}
