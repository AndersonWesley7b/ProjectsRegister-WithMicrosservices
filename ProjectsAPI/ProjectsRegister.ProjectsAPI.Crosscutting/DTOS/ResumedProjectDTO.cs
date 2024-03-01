namespace ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
public class ResumedProjectDTO
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

}
