using Microsoft.EntityFrameworkCore;
using Events.Api.Entities;

namespace Events.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<User> Users => Set<User>();
    public DbSet<Admin> Admins => Set<Admin>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Organizer> Organizers => Set<Organizer>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<TicketType> TicketTypes => Set<TicketType>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Sponsor> Sponsors => Set<Sponsor>();
    public DbSet<EventSponsorship> EventSponsorships => Set<EventSponsorship>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().UseTptMappingStrategy();
        modelBuilder.Entity<EventSponsorship>()
        .HasKey(x => new { x.EventId, x.SponsorId });
    }
}