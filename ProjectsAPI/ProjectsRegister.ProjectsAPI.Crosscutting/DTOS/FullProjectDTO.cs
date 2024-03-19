using ProjectsRegister.ProjectsAPI.Crosscutting.DTOS.Interfaces;

namespace ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
public sealed class FullProjectDTO : IProjectDTO
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ProjectLink { get; set; } = string.Empty;

    public string RepositoryLink { get; set; } = string.Empty;

    public string MediaLink { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;
}
