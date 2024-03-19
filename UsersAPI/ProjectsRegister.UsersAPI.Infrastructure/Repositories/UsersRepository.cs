using Microsoft.EntityFrameworkCore;
using ProjectsRegister.UsersAPI.Domain.Entities;
using ProjectsRegister.UsersAPI.Infrastructure.Repositories.IRepositories;
using UsersAPI.Infraestructure.Context;

namespace ProjectsRegister.UsersAPI.Infrastructure.Repositories;
public sealed class UsersRepository : IUsersRepository
{
    private readonly SqlServerContext _context;

    public UsersRepository(SqlServerContext context)
    {
        _context = context;   
    }

    public IQueryable<User?> GetAllUsersReadOnly()
    {
        return _context.Users.AsNoTracking();
    }

    public IQueryable<User?> GetAllUsers()
    {
        return _context.Users;
    }

    public async Task<User?> GetUserById(Guid _Id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserId == _Id);
    }

    public async Task<User?> GetUserByIdReadOnly(Guid _Id)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == _Id);
    }

    public async Task<bool> CheckUserExists(Guid _Id)
    {
        return await _context.Users.AsNoTracking().AnyAsync(x => x.UserId == _Id);
    }

    public async Task<string> GetUserNameByIdReadOnly(Guid _Id)
    {
        return await _context.Users.AsNoTracking().Where(x => x.UserId == _Id).Select(x => x.UserName).FirstOrDefaultAsync() ?? string.Empty;
    }

    public async Task AddUser(User _User)
    {
        await _context.Users.AddAsync(_User);
    }

    public void DeleteUser(User _User)
    {
        _context.Users.Remove(_User);
    }

}
