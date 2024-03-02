using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Infraestructure.Context;
using ProjectsRegister.ProjectsAPI.Domain.Entities;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Repositories;
public sealed class ProjectRepository : IProjectRepository
{
    private readonly SqlServerContext _context;

    public ProjectRepository(SqlServerContext context)
    {
        _context = context;   
    }

    public IQueryable<Project?> GetAllProjectsReadOnly()
    {
        return _context.Projects.AsNoTracking();
    }

    public IQueryable<Project?> GetAllProjects()
    {
        return _context.Projects;
    }

    public async Task<Project?> GetProjectById(Guid _Id)
    {
        return await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == _Id);
    }

    public async Task AddProject(Project _Project)
    {
        await _context.Projects.AddAsync(_Project);
    }

    public async Task DeleteProjectById(Guid _Id)
    {
        await GetProjectById(_Id);
    }

    public async Task CommitChanges()
    {
        await _context.SaveChangesAsync();
    }

}
