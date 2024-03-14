namespace ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
public sealed class ResumedProjectDTO
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

}
