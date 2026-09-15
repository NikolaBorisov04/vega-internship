using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Api.Domain.Entities;

[Table("Admins")]
public class Admin : Organizer
{
    public ICollection<Organizer> ValidatedOrganizers { get; set; } = new List<Organizer>();
}