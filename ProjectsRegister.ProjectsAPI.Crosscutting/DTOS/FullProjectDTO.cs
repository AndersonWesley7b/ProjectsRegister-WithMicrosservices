using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectsRegister.ProjectsAPI.Crosscutting.DTOS;
public class FullProjectDTO
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ProjectLink { get; set; } = string.Empty;

    public string RepositoryLink { get; set; } = string.Empty;

    public string MediaLink { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    public int UserId { get; set; }

    public string UserName { get; set; } = string.Empty;
}
