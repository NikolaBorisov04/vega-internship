using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Entities;

[Table("EventPhotos")]
public class EventPhoto : AuditableEntity
{
    [MaxLength(1000)]
    [Required]
    public string Url { get; set; }

    [MaxLength(100)]
    public string? Caption { get; set; }

    public Guid EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event? Event { get; set; }
}
