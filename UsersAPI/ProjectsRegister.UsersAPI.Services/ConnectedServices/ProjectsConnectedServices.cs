using ProjectsRegister.UsersAPI.Services.ConnectedServices.IConnectedServices;
using System.Net.Http.Json;

namespace ProjectsRegister.ProjectsAPI.Services.ConnectedServices;
public sealed class ProjectsConnectedServices : IProjectsConnectedServices
{
    private readonly HttpClient _client;
    public const string BasePath = "api/Projects";

    public ProjectsConnectedServices(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task DeleteProjectsByUserId(Guid _UserId)
    {
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{BasePath}/DeleteProjectsByUserId/{_UserId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync() ?? "Houve um erro ao se conectar com o serviço de usuários!");

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
