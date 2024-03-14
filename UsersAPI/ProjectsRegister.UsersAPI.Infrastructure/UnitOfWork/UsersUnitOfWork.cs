using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
using ProjectsRegister.UsersAPI.Infrastructure.Repositories.IRepositories;
using UsersAPI.Infraestructure.Context;

namespace UsersRegister.UsersAPI.Infrastructure.UnitOfWork
{
    public class UsersUnitOfWork : IUsersUnitOfWork
    {
        private readonly SqlServerContext _context;
        private readonly IUsersRepository _userRepository;

        public UsersUnitOfWork(SqlServerContext context, IUsersRepository UserRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userRepository = UserRepository ?? throw new ArgumentNullException(nameof(UserRepository));
        }

        public IUsersRepository UserRepository => _userRepository;

        public IUsersRepository userRepository => throw new NotImplementedException();

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

    }
}
