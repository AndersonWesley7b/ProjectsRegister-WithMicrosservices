using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;
using System.Net;
using System.Net.Http.Json;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public sealed class UsersApplicationServices : IUsersApplicationServices
{
    private readonly HttpClient _client;
    public const string BasePath = "api/Users";

    public UsersApplicationServices(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task CheckUserExists(Guid _Id)
    {
        try
        {
            HttpResponseMessage response = await _client.GetAsync($"{BasePath}/CheckUserExists/{_Id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception(await response.Content.ReadAsStringAsync() ?? "Houve um erro ao se conectar com o serviço de usuários!");

            bool userExists = await response.Content.ReadFromJsonAsync<bool>();

            if (!userExists)
                throw new Exception("O usuário selecionado não existe");

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
