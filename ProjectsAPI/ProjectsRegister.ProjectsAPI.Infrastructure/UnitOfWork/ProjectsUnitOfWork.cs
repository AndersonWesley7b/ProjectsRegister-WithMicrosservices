using ProjectsAPI.Infraestructure.Context;
using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork
{
    public class ProjectsUnitOfWork : IProjectsUnitOfWork
    {
        private readonly SqlServerContext _context;
        private readonly IProjectsRepository _projectRepository;

        public ProjectsUnitOfWork(SqlServerContext context, IProjectsRepository projectRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public IProjectsRepository ProjectRepository => _projectRepository;

        public IProjectsRepository projectRepository => throw new NotImplementedException();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
