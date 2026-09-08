using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Entities;

[Table("Admins")]
public class Admin : Organizer
{
    public ICollection<Organizer> ValidatedOrganizers { get; set; } = new List<Organizer>();
}