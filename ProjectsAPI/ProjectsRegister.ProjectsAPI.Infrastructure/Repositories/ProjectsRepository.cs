using Microsoft.EntityFrameworkCore;
using ProjectsAPI.Infraestructure.Context;
using ProjectsRegister.ProjectsAPI.Domain.Entities;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.Repositories;
public sealed class ProjectsRepository : IProjectsRepository
{
    private readonly SqlServerContext _context;

    public ProjectsRepository(SqlServerContext context)
    {
        _context = context;   
    }

    public IQueryable<Project?> GetAllProjectsReadOnly()
        => _context.Projects.AsNoTracking();


    public IQueryable<Project?> GetAllProjects()
        => _context.Projects;
    

    public async Task<Project?> GetProjectByIdReadOnly(Guid _Id)
        => await _context.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.ProjectId == _Id);


    public async Task<Project?> GetProjectById(Guid _Id)
        => await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == _Id);


    public IQueryable<Project> GetProjectsByUserId(Guid _UserId)
        => _context.Projects.Where(x => x.UserId == _UserId);

    
    public IQueryable<Project> GetProjectsByUserIdReadOnly(Guid _UserId)
        => _context.Projects.AsNoTracking().Where(x => x.UserId == _UserId);

    public async Task<bool> CheckProjectExists(Guid _Id)
        =>await _context.Projects.AsNoTracking().AnyAsync(x => x.ProjectId == _Id);

    public async Task AddProject(Project _Project)
        => await _context.Projects.AddAsync(_Project);


    public void DeleteProject(Project _Project)
         => _context.Projects.Remove(_Project);

    public void DeleteProjects(List<Project> _Projects)
         => _context.Projects.RemoveRange(_Projects);

}
