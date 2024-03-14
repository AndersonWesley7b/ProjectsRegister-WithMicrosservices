using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
using ProjectsRegister.UsersAPI.Crosscutting.DTOS;
using ProjectsRegister.UsersAPI.Domain.Entities;
using ProjectsRegister.UsersAPI.Infrastructure.Repositories.IRepositories;
using ProjectsRegister.UsersAPI.Services.ApplicationServices.IApplicationServices;

namespace ProjectsRegister.UsersAPI.Services.ApplicationServices;
public sealed class UsersApplicationServices : BaseApplicationServices, IUsersApplicationServices 
{
    private readonly IUsersUnitOfWork _uow;

    public UsersApplicationServices(IUsersUnitOfWork uow)
    {
        _uow = uow;
    }

    #region Queries

    private IQueryable<SelectDTO> GetUsersForSelectQuery()
    {
        IQueryable<User?> queryUsers = _uow.userRepository.GetAllUsersReadOnly();

        IQueryable<SelectDTO> query = (from User in queryUsers
                                               select new SelectDTO
                                               {
                                                   Id = User.UserId,
                                                   Name = User.UserName,
                                               });

        return query;

    }

    #endregion

    #region DatabaseOperation

    public async Task<List<SelectDTO>> GetUsersForSelect()
    {
        List<SelectDTO> Users = await GetUsersForSelectQuery().ToListAsync();
        return Users;
    }

    public async Task CreateNewUser(CreateUserDTO _NewUser, bool _Commit = false)
    {
        NewUserValidate(_NewUser);

        User user = new()
        { 
            UserName = _NewUser.UserName,
            CreatedOn = DateTime.UtcNow,
            About = _NewUser.About,
            BirthDate = _NewUser.BirthDate.Date,
            Email = _NewUser.Email,
        };

        ValidateModel(user);

        await _uow.userRepository.AddUser(user);

        if (_Commit)
            await _uow.userRepository.CommitChanges();
    }

    public async Task<bool> CheckUserExists(Guid _UserId)
    {
        if (await _uow.userRepository.CheckUserExists(_UserId))
            return true;
        return false;
    }

    public async Task<string> GetUserNameByIdReadOnly(Guid _Id)
    {
        string userName = await _uow.userRepository.GetUserNameByIdReadOnly(_Id);
        return userName;
    }

    #endregion

    #region Validations
    private static void NewUserValidate(CreateUserDTO _NewUser)
    {
        if (Equals(_NewUser, null))
            throw new Exception("Preencha os dados do projeto corretamente, para realizar um novo cadastro!");

        if (_NewUser.BirthDate > DateTime.Today)
            throw new Exception("A data informada deve ser menor que a data atual!");

        if (!EmailValidate(_NewUser.Email))
            throw new Exception("Informe um e-mail válido!");
    }

    #endregion

}
