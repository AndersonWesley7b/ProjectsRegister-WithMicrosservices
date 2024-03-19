namespace ProjectsRegister.UsersAPI.Crosscutting.DTOS.Interfaces;
public interface IUserDTO
{
    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

    public string UserName { get; set; }

    public string About { get; set; }
}
