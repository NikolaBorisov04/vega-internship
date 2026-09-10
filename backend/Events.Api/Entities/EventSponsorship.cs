using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Entities;
[Table("EventSponsorships")]
public class EventSponsorship : AuditableEntity
{
    [Column(TypeName = "numeric(18,2)")]
    public decimal ContributionAmount { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public Guid EventId {get; set; }

    [ForeignKey(nameof(EventId))]
    public Event? Event {get; set;}

    public Guid SponsorId {get; set;}
    
    [ForeignKey(nameof(SponsorId))]
    public Sponsor? Sponsor {get; set;}
}