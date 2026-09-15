using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Domain.Entities;

[Table("Users")]
[Index(nameof(Email), IsUnique = true)]
public class User : AuditableEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    public UserRole Role { get; set; }
}