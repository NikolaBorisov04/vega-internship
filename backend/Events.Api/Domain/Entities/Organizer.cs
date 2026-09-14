using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Entities;

[Table("Organizers")]
public class Organizer : Customer
{
    [MaxLength(200)]
    public string CompanyName { get; set; } = string.Empty;

    public Guid? ValidatedById {get; set;}

    [ForeignKey(nameof(ValidatedById))]
    public Admin? ValidatedByAdmin {get; set;}

    [Required]
    public bool Validated {get; set;} = false;

    public ICollection<Event> OrganizedEvents { get; set; } = new List<Event>();
}