namespace ProjectsRegister.ProjectsAPI.Services.ConnectedServices.IConnectedServices;
public interface IUsersConnectedServices
{
    Task CheckUserExists(Guid _Id);

}
