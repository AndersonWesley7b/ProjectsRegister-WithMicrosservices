using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectsRegister.ProjectsAPI.Domain.Entities;
public class Project
{
    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(300)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "O link do projeto é obrigatório para o cadastro!")]
    public string ProjectLink { get; set; } = string.Empty;

    [Required(ErrorMessage = "O link do repositório do projeto é obrigatório para o cadastro!")]
    public string RepositoryLink { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
}
