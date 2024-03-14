using ProjectsRegister.ProjectsAPI.Infrastructure.Repositories.IRepositories;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
public interface  IProjectsUnitOfWork
{
    IProjectsRepository projectRepository { get;}

    Task SaveChangesAsync();
}
