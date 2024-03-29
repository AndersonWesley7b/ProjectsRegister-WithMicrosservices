﻿using ProjectsRegister.UsersAPI.Crosscutting.DTOS;

namespace ProjectsRegister.UsersAPI.Services.ApplicationServices.IApplicationServices;
public interface IUsersApplicationServices
{
    Task<List<SelectDTO>> GetUsersForSelect();

    Task CreateNewUser(CreateUserDTO _NewUser, bool _Commit);

    Task UpdateUser(FullUserDTO _NewUser, bool _Commit);

    Task DeleteUser(Guid _UserId, bool _Commit);

    Task<bool>CheckUserExists(Guid _UserId);

    Task <string> GetUserNameByIdReadOnly(Guid _UserId);
}
