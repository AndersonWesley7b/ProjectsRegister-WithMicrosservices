using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
public interface  IProjectsUnitOfWork
{
    IProjectsRepository ProjectRepository { get;}

    Task SaveChangesAsync();
}
