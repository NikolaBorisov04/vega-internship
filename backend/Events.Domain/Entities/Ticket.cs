using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Domain.Entities;

[Table("Tickets")]
public class Ticket : AuditableEntity
{
    [MaxLength(64)]
    public string TicketCode {get; set;} = Guid.NewGuid().ToString("N");

    [MaxLength(1000)]
    public string? QRCodeURL {get; set;}

    public int? SeatNumber {get; set;}

    public bool IsUsed { get; set; } = false;

    public DateTimeOffset? UsedAt { get; set; }

    public Guid TicketTypeId {get; set; }

    [ForeignKey(nameof(TicketTypeId))]
    public TicketType? TicketType {get; set;}

    public Guid CustomerId {get; set;}

    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer {get; set;}
}