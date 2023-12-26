using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Cms.Infrastructure.Persistence.Entities;

public class Role : IdentityRole<Guid>
{
    [MaxLength(200)]
    public string RoleDesc { get; set; }
    public int Order { get; set; }
    public bool IsVisible { get; set; }
}