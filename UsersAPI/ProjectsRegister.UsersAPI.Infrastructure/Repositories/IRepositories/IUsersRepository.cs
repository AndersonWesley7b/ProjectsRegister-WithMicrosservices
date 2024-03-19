using ProjectsRegister.UsersAPI.Domain.Entities;

namespace ProjectsRegister.UsersAPI.Infrastructure.Repositories.IRepositories;
public interface IUsersRepository
{
    IQueryable<User?> GetAllUsersReadOnly();

    IQueryable<User?> GetAllUsers();

    Task<User?> GetUserById(Guid _Id);

    Task<User?> GetUserByIdReadOnly(Guid _Id);

    Task<bool> CheckUserExists(Guid _Id);

    Task<string> GetUserNameByIdReadOnly(Guid _Id);

    Task AddUser(User _User);

    void DeleteUser(User _User);

}
