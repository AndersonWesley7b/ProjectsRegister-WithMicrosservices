using ProjectsRegister.UsersAPI.Infrastructure.Repositories.IRepositories;

namespace ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
public interface  IUsersUnitOfWork
{
    IUsersRepository userRepository { get;}

    Task SaveChangesAsync();
}
