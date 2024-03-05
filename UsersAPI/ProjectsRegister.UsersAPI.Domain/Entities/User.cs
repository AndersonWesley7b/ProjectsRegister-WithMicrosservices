using System.ComponentModel.DataAnnotations;

namespace ProjectsRegister.UsersAPI.Domain.Entities;
public sealed class User
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    public string About { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }
}
