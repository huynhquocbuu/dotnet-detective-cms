using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Cms.Infrastructure.Persistence.Entities;

public class User : IdentityUser<Guid>
{
    [Required]
    public char AuthType { get; set; }

    [MaxLength(200)]
    public string Fullname { get; set; }
    public bool IsActive { get; set; }
}