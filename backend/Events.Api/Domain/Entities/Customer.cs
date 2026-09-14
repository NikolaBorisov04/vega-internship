using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Entities;

[Table("Customers")]
public class Customer : User
{
    [MaxLength(100)]
    public string Country {get; set;}

    [MaxLength(100)]
    public string City {get; set;}

    [MaxLength(100)]
    public string Address { get; set; }

    [MaxLength(30)]
    public string PhoneNumber {get; set;}
}
