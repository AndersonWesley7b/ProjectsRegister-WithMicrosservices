using ProjectsRegister.UsersAPI.Crosscutting.DTOS.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ProjectsRegister.UsersAPI.Crosscutting.DTOS;
public sealed class FullUserDTO :IUserDTO
{
    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string About { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public DateTime BirthDate { get; set; }
}
