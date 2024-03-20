using Microsoft.EntityFrameworkCore;
using ProjectsRegister.ProjectsAPI.Infrastructure.UnitOfWork.IUnitOfWork;
using ProjectsRegister.UsersAPI.Crosscutting.DTOS;
using ProjectsRegister.UsersAPI.Crosscutting.DTOS.Interfaces;
using ProjectsRegister.UsersAPI.Domain.Entities;
using ProjectsRegister.UsersAPI.Services.ApplicationServices.IApplicationServices;
using ProjectsRegister.UsersAPI.Services.ConnectedServices.IConnectedServices;

namespace ProjectsRegister.UsersAPI.Services.ApplicationServices;
public sealed class UsersApplicationServices : BaseApplicationServices, IUsersApplicationServices 
{
    private readonly IUsersUnitOfWork _uow;
    private readonly IProjectsConnectedServices _projectsConnectedServices;

    public UsersApplicationServices(IUsersUnitOfWork uow, IProjectsConnectedServices projectsConnectedServices)
    {
        _uow = uow;
        _projectsConnectedServices = projectsConnectedServices;
    }

    #region Queries

    private IQueryable<SelectDTO> GetUsersForSelectQuery()
    {
        IQueryable<User?> queryUsers = _uow.UserRepository.GetAllUsersReadOnly();

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
        try
        {
            List<SelectDTO> Users = await GetUsersForSelectQuery().ToListAsync();
            return Users;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
    }

    public async Task CreateNewUser(CreateUserDTO _NewUser, bool _Commit = false)
    {
        UserValidate(_NewUser);

        User user = new()
        { 
            UserName = _NewUser.UserName,
            CreatedOn = DateTime.UtcNow,
            About = _NewUser.About,
            BirthDate = _NewUser.BirthDate.Date,
            Email = _NewUser.Email,
        };

        ValidateModel(user);

        await _uow.UserRepository.AddUser(user);

        if (_Commit)
            await _uow.SaveChangesAsync();
    }

    public async Task UpdateUser(FullUserDTO _UpdateUser, bool _Commit = false)
    {
        if (_UpdateUser == null)
            throw new ArgumentNullException(nameof(_UpdateUser), "O objeto de usuário para atualização não pode ser nulo.");

        User userToUpdate = await _uow.UserRepository.GetUserById(_UpdateUser.UserId) ?? throw new NullReferenceException("Selecione um usuário válido para realizar o update!");

        UserValidate(_UpdateUser);

        userToUpdate.UserName = _UpdateUser.UserName;
        userToUpdate.About = _UpdateUser.About;
        userToUpdate.BirthDate = _UpdateUser.BirthDate.Date;
        userToUpdate.Email = _UpdateUser.Email;
        userToUpdate.LastUpdatedOn = DateTime.UtcNow;

        ValidateModel(userToUpdate);

        if (_Commit)
            await _uow.SaveChangesAsync();
    }

    public async Task DeleteUser(Guid _UserId, bool _Commit)
    {
        if (Equals(_UserId, Guid.Empty))
            throw new Exception("O Id do projeto informado é inválido!");

        User userToDelete = await _uow.UserRepository.GetUserById(_UserId) ?? throw new NullReferenceException("Selecione um usuário válido para excluir!");

        await _projectsConnectedServices.DeleteProjectsByUserId(_UserId);

        _uow.UserRepository.DeleteUser(userToDelete);

        if (_Commit)
            await _uow.SaveChangesAsync();
    }

    public async Task<bool> CheckUserExists(Guid _UserId)
    {
        if (await _uow.UserRepository.CheckUserExists(_UserId))
            return true;
        return false;
    }

    public async Task<string> GetUserNameByIdReadOnly(Guid _Id)
    {
        string userName = await _uow.UserRepository.GetUserNameByIdReadOnly(_Id);
        return userName;
    }

    #endregion

    #region Validations
    private static void UserValidate(IUserDTO _User)
    {
        if (Equals(_User, null))
            throw new NullReferenceException("Preencha os dados do projeto corretamente, para realizar um novo cadastro!");

        if (_User.BirthDate > DateTime.Today)
            throw new Exception("A data informada deve ser menor que a data atual!");

        if (!EmailValidate(_User.Email))
            throw new Exception("Informe um e-mail válido!");
    }

    #endregion

}
