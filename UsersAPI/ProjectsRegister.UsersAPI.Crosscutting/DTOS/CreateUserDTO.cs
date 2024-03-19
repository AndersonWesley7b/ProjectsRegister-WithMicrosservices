using ProjectsRegister.UsersAPI.Crosscutting.DTOS.Interfaces;

namespace ProjectsRegister.UsersAPI.Crosscutting.DTOS;
public class CreateUserDTO : IUserDTO
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string About { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }
}
