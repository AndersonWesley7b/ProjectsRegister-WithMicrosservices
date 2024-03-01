using ProjectsRegister.ProjectsAPI.Domain.Entities;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
public interface IProjectRepository
{
    IQueryable<Project?> GetAllProjectsReadOnly();

    IQueryable<Project?> GetAllProjects();

    Task<Project?> GetProjectById(Guid _Id);

    void AddProject(Project _Project);

    void DeleteProjectById(Guid _Id);
}
