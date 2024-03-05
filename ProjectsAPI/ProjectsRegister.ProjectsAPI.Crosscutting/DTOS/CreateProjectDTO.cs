namespace ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
public sealed class CreateProjectDTO
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ProjectLink { get; set; } = string.Empty;

    public string RepositoryLink { get; set; } = string.Empty;

    public string MediaLink { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;
}
