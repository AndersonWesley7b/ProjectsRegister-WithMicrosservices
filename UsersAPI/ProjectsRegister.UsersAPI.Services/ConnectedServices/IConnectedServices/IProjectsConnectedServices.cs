namespace ProjectsRegister.UsersAPI.Services.ConnectedServices.IConnectedServices;
public interface IProjectsConnectedServices
{
    Task DeleteProjectsByUserId(Guid _UserId);
}
