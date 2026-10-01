//EF -> nisu migracije samo, LINQ, .............
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Domain.Entities;

[Table("Sponsors")]
[Index(nameof(ContactEmail), IsUnique = true)]
public class Sponsor : AuditableEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string ContactEmail { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    [MaxLength(200)]
    public string WebsiteUrl { get; set; }

    [MaxLength(1000)]
    [Required]
    public string ImageUrl { get; set; }

    [MaxLength(1000)]
    [Required]
    public string ImagePublicId { get; set; }

    [Required]
    [MaxLength(100)]
    public string TaxId { get; set; }

    [Required]
    public bool IsActive { get; set; } = true;
    
    public ICollection<EventSponsorship> SponsoredEvents { get; set; } = new List<EventSponsorship>();
}