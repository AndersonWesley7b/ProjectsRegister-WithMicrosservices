using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectsRegister.ProjectsAPI.Domain.Entities;
public sealed class User
{
    [Required]
    public int UserId { get; set; }

    [StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }
}
