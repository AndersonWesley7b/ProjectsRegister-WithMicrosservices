namespace ProjectsRegister.ProjectsAPI.Crosscutting.DTOS.Interfaces;
public interface IProjectDTO
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string ProjectLink { get; set; }

    public string RepositoryLink { get; set; }

    public string MediaLink { get; set; }

    public Guid UserId { get; set; }

}
