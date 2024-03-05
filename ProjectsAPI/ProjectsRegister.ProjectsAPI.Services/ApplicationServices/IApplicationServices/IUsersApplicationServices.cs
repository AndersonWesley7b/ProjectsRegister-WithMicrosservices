namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;
public interface IUsersApplicationServices
{
    Task CheckUserExists(Guid _Id);
}
