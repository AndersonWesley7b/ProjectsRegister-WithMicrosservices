using ProjectsRegister.ProjectsAPI.Services.ApplicationServices.IApplicationServices;

namespace ProjectsRegister.ProjectsAPI.Services.ApplicationServices;
public sealed class UsersApplicationServices : IUsersApplicationServices
{
    private readonly HttpClient _client;
    public const string BasePath = "api/User";

    public UsersApplicationServices(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task CheckUserExists(Guid _Id)
    {
        HttpResponseMessage response = await _client.GetAsync($"{BasePath}/CheckUserExists/{_Id}");

        if(!response.IsSuccessStatusCode)
            throw new Exception("Opa! Parece que tivemos um problema para acessar os usuários. Entre em contato com nosso suporte");
    }
}
