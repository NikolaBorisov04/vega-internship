using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Entities;

[Table("Events")]
public class Event : AuditableEntity
{
    [MaxLength(100)]
    [Required]
    public string Title { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; }

    [MaxLength(100)]
    [Required]
    public string Country { get; set; }

    [MaxLength(100)]
    [Required]
    public string City { get; set; }

    [MaxLength(100)]
    [Required]
    public string Address { get; set; }

    [Required]
    [MaxLength(200)]
    public string MainImageURL {get; set;}

    [MaxLength(100)]
    public string VenueName { get; set; }

    [Required]
    public DateTimeOffset DateAndTimeOfEvent { get; set; }

    public ICollection<TicketType> TicketTypes {get; set;} = new List<TicketType>();

    public ICollection<EventSponsorship> Sponsorships { get; set; } = new List<EventSponsorship>();

    public ICollection<string> EventPhotosURL { get; set; } = new List<string>();

    public Guid OrganizerId { get; set; }

    [ForeignKey(nameof(OrganizerId))]
    public Organizer? Organizer { get; set; }
}
