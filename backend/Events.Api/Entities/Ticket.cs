using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Entities;

[Table("Tickets")]
public class Ticket : AuditableEntity
{
    [Required]
    [MaxLength(64)]
    public string TicketCode {get; set;} = Guid.NewGuid().ToString("N");

    [MaxLength(1000)]
    public string QRCodeURL {get; set;}

    public int? SeatNumber {get; set;}

    public bool IsUsed { get; set; } = false;

    public DateTime? UsedAt { get; set; }

    public Guid TicketTypeId {get; set; }

    [ForeignKey(nameof(TicketTypeId))]
    public TicketType? TicketType {get; set;}

    public Guid CustomerId {get; set;}
    
    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer {get; set;}
}