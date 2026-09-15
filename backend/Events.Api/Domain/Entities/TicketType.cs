using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Api.Domain.Entities;

[Table("TicketTypes")]
public class TicketType : AuditableEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    [Column(TypeName = "numeric(18,2)")]
    [Required]
    public decimal Price { get; set; }

    [Required]
    public int QuantityAvailable { get; set; }

    [MaxLength(1000)]
    public string? TicketBackgroundImageUrl { get; set; }
    
    [Required]
    public Guid EventId { get; set; }

    [ForeignKey(nameof(EventId))]
    public Event? Event { get; set; }

    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}