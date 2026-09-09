using Microsoft.EntityFrameworkCore;
using Events.Api.Entities;

namespace Events.Api.Data;

public static class TestDataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        var createdAt = new DateTimeOffset(
            2026, 9, 1, 12, 0, 0,
            TimeSpan.Zero);

        // ============================================================
        // ADMIN
        // ============================================================

        var admin = new Admin
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            Name = "Alex Johnson",
            Email = "admin@test.com",
            PasswordHash = "mock-password-hash-admin",
            Role = UserRole.Admin,

            Country = "Serbia",
            City = "Nis",
            Address = "Main Street 1",
            PhoneNumber = "+381600000001",

            CompanyName = "EventTix Administration",
            Validated = true,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        // ============================================================
        // CUSTOMERS
        // ============================================================

        var customer1 = new Customer
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000101"),
            Name = "Mark Brown",
            Email = "mark@test.com",
            PasswordHash = "mock-password-hash-001",
            Role = UserRole.Customer,

            Country = "Serbia",
            City = "Nis",
            Address = "Oak Street 12",
            PhoneNumber = "+381600000101",

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var customer2 = new Customer
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000102"),
            Name = "Sarah Wilson",
            Email = "sarah@test.com",
            PasswordHash = "mock-password-hash-002",
            Role = UserRole.Customer,

            Country = "Serbia",
            City = "Belgrade",
            Address = "King Street 24",
            PhoneNumber = "+381600000102",

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var customer3 = new Customer
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000103"),
            Name = "Daniel Smith",
            Email = "daniel@test.com",
            PasswordHash = "mock-password-hash-003",
            Role = UserRole.Customer,

            Country = "Serbia",
            City = "Kragujevac",
            Address = "Green Street 7",
            PhoneNumber = "+381600000103",

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        // ============================================================
        // ORGANIZERS
        // ============================================================

        var organizer1 = new Organizer
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000201"),
            Name = "Michael Carter",
            Email = "michael@test.com",
            PasswordHash = "mock-password-hash-004",
            Role = UserRole.Organizer,

            Country = "Serbia",
            City = "Belgrade",
            Address = "River Street 15",
            PhoneNumber = "+381600000201",

            CompanyName = "Urban Events",
            Validated = true,
            ValidatedById = admin.Id,
            ValidatedByAdmin = admin,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var organizer2 = new Organizer
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000202"),
            Name = "Emma Davis",
            Email = "emma@test.com",
            PasswordHash = "mock-password-hash-005",
            Role = UserRole.Organizer,

            Country = "Serbia",
            City = "Novi Sad",
            Address = "Central Street 8",
            PhoneNumber = "+381600000202",

            CompanyName = "Future Events",
            Validated = true,
            ValidatedById = admin.Id,
            ValidatedByAdmin = admin,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        admin.ValidatedOrganizers.Add(organizer1);
        admin.ValidatedOrganizers.Add(organizer2);

        context.Add(admin);

        context.AddRange(
            customer1,
            customer2,
            customer3,
            organizer1,
            organizer2);

        context.SaveChanges();

        // ============================================================
        // SPONSORS
        // ============================================================

        var sponsor1 = new Sponsor
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000301"),
            Name = "TechWorld",
            ContactEmail = "contact@techworld.com",
            Description = "Technology company supporting community events.",
            WebsiteUrl = "https://techworld.com",
            LogoUrl = "https://example.com/logos/techworld.png",
            TaxId = "10000001",
            IsActive = true,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var sponsor2 = new Sponsor
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000302"),
            Name = "FreshDrink",
            ContactEmail = "contact@freshdrink.com",
            Description = "Beverage company supporting music and culture events.",
            WebsiteUrl = "https://freshdrink.com",
            LogoUrl = "https://example.com/logos/freshdrink.png",
            TaxId = "10000002",
            IsActive = true,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var sponsor3 = new Sponsor
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000303"),
            Name = "CityBank",
            ContactEmail = "contact@citybank.com",
            Description = "Financial institution supporting local events.",
            WebsiteUrl = "https://citybank.com",
            LogoUrl = "https://example.com/logos/citybank.png",
            TaxId = "10000003",
            IsActive = true,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        context.Sponsor.AddRange(
            sponsor1,
            sponsor2,
            sponsor3);

        context.SaveChanges();

        // ============================================================
        // EVENTS
        // ============================================================

        var event1 = new Event
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000401"),
            Title = "Belgrade Music Festival",
            Description = "A three day music festival with rock, jazz and electronic artists.",
            Country = "Serbia",
            City = "Belgrade",
            Address = "Park Arena 10",
            MainImageURL = "https://example.com/events/belgrade-music.jpg",
            VenueName = "Park Arena",
            DateAndTimeOfEvent = new DateTimeOffset(
                2026, 9, 20, 20, 0, 0,
                TimeSpan.Zero),

            EventPhotosURL = new List<string>
            {
                "https://example.com/events/belgrade-music-1.jpg",
                "https://example.com/events/belgrade-music-2.jpg",
                "https://example.com/events/belgrade-music-3.jpg"
            },

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var event2 = new Event
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000402"),
            Title = "Nis Tech Conference",
            Description = "A technology conference focused on software development and modern platforms.",
            Country = "Serbia",
            City = "Nis",
            Address = "Innovation Center 5",
            MainImageURL = "https://example.com/events/nis-tech.jpg",
            VenueName = "Innovation Center",
            DateAndTimeOfEvent = new DateTimeOffset(
                2026, 10, 10, 10, 0, 0,
                TimeSpan.Zero),

            EventPhotosURL = new List<string>
            {
                "https://example.com/events/nis-tech-1.jpg",
                "https://example.com/events/nis-tech-2.jpg"
            },

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var event3 = new Event
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000403"),
            Title = "Novi Sad Jazz Night",
            Description = "An evening of modern jazz performances from local and international artists.",
            Country = "Serbia",
            City = "Novi Sad",
            Address = "Culture Hall 22",
            MainImageURL = "https://example.com/events/novi-sad-jazz.jpg",
            VenueName = "Culture Hall",
            DateAndTimeOfEvent = new DateTimeOffset(
                2026, 10, 25, 19, 30, 0,
                TimeSpan.Zero),

            EventPhotosURL = new List<string>
            {
                "https://example.com/events/novi-sad-jazz-1.jpg",
                "https://example.com/events/novi-sad-jazz-2.jpg"
            },

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };


        organizer1.OrganizedEvents.Add(event1);
        organizer1.OrganizedEvents.Add(event2);
        organizer2.OrganizedEvents.Add(event3);

        context.Events.AddRange(
            event1,
            event2,
            event3);

        context.SaveChanges();

        // ============================================================
        // TICKET TYPES
        // ============================================================

        var ticketType1 = new TicketType
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000501"),
            Name = "General Admission",
            Description = "Standard entrance ticket.",
            Price = 2500m,
            QuantityAvailable = 500,
            TicketBackgroundImageUrl = "https://example.com/tickets/general.jpg",
            EventId = event1.Id,
            Event = event1,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticketType2 = new TicketType
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000502"),
            Name = "VIP",
            Description = "VIP entrance with reserved area access.",
            Price = 5000m,
            QuantityAvailable = 100,
            TicketBackgroundImageUrl = "https://example.com/tickets/vip.jpg",
            EventId = event1.Id,
            Event = event1,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticketType3 = new TicketType
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000503"),
            Name = "Student",
            Description = "Discounted ticket for students.",
            Price = 1500m,
            QuantityAvailable = 200,
            TicketBackgroundImageUrl = "https://example.com/tickets/student.jpg",
            EventId = event2.Id,
            Event = event2,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticketType4 = new TicketType
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000504"),
            Name = "Professional",
            Description = "Full access conference ticket.",
            Price = 4500m,
            QuantityAvailable = 300,
            TicketBackgroundImageUrl = "https://example.com/tickets/professional.jpg",
            EventId = event2.Id,
            Event = event2,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticketType5 = new TicketType
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000505"),
            Name = "Regular",
            Description = "Regular entrance ticket.",
            Price = 2200m,
            QuantityAvailable = 150,
            TicketBackgroundImageUrl = "https://example.com/tickets/regular.jpg",
            EventId = event3.Id,
            Event = event3,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticketType6 = new TicketType
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000506"),
            Name = "Premium",
            Description = "Premium entrance ticket with reserved seating.",
            Price = 3800m,
            QuantityAvailable = 50,
            TicketBackgroundImageUrl = "https://example.com/tickets/premium.jpg",
            EventId = event3.Id,
            Event = event3,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        context.TicketTypes.AddRange(
            ticketType1,
            ticketType2,
            ticketType3,
            ticketType4,
            ticketType5,
            ticketType6);

        context.SaveChanges();

        // ============================================================
        // TICKETS
        // ============================================================

        var ticket1 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000601"),
            TicketCode = "TICKET-0001",
            QRCodeURL = "https://example.com/qr/TICKET-0001",
            SeatNumber = 101,
            IsUsed = true,
            UsedAt = new DateTime(
                2026, 9, 20, 19, 45, 0,
                DateTimeKind.Utc),

            TicketTypeId = ticketType1.Id,
            TicketType = ticketType1,

            CustomerId = customer1.Id,
            Customer = customer1,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket2 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000602"),
            TicketCode = "TICKET-0002",
            QRCodeURL = "https://example.com/qr/TICKET-0002",
            SeatNumber = 102,
            IsUsed = false,

            TicketTypeId = ticketType1.Id,
            TicketType = ticketType1,

            CustomerId = customer2.Id,
            Customer = customer2,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket3 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000603"),
            TicketCode = "TICKET-0003",
            QRCodeURL = "https://example.com/qr/TICKET-0003",
            SeatNumber = 1,
            IsUsed = false,

            TicketTypeId = ticketType2.Id,
            TicketType = ticketType2,

            CustomerId = customer3.Id,
            Customer = customer3,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket4 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000604"),
            TicketCode = "TICKET-0004",
            QRCodeURL = "https://example.com/qr/TICKET-0004",
            SeatNumber = 15,
            IsUsed = false,

            TicketTypeId = ticketType3.Id,
            TicketType = ticketType3,

            CustomerId = customer1.Id,
            Customer = customer1,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket5 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000605"),
            TicketCode = "TICKET-0005",
            QRCodeURL = "https://example.com/qr/TICKET-0005",
            SeatNumber = 16,
            IsUsed = false,

            TicketTypeId = ticketType4.Id,
            TicketType = ticketType4,

            CustomerId = customer2.Id,
            Customer = customer2,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket6 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000606"),
            TicketCode = "TICKET-0006",
            QRCodeURL = "https://example.com/qr/TICKET-0006",
            SeatNumber = 30,
            IsUsed = true,
            UsedAt = new DateTime(
                2026, 10, 10, 9, 50, 0,
                DateTimeKind.Utc),

            TicketTypeId = ticketType4.Id,
            TicketType = ticketType4,

            CustomerId = customer3.Id,
            Customer = customer3,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket7 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000607"),
            TicketCode = "TICKET-0007",
            QRCodeURL = "https://example.com/qr/TICKET-0007",
            SeatNumber = 50,
            IsUsed = false,

            TicketTypeId = ticketType5.Id,
            TicketType = ticketType5,

            CustomerId = customer1.Id,
            Customer = customer1,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket8 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000608"),
            TicketCode = "TICKET-0008",
            QRCodeURL = "https://example.com/qr/TICKET-0008",
            SeatNumber = 51,
            IsUsed = false,

            TicketTypeId = ticketType5.Id,
            TicketType = ticketType5,

            CustomerId = customer2.Id,
            Customer = customer2,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket9 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000609"),
            TicketCode = "TICKET-0009",
            QRCodeURL = "https://example.com/qr/TICKET-0009",
            SeatNumber = 3,
            IsUsed = false,

            TicketTypeId = ticketType6.Id,
            TicketType = ticketType6,

            CustomerId = customer3.Id,
            Customer = customer3,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        var ticket10 = new Ticket
        {
            Id = Guid.Parse("00000000-0000-0000-0000-000000000610"),
            TicketCode = "TICKET-0010",
            QRCodeURL = "https://example.com/qr/TICKET-0010",
            SeatNumber = 4,
            IsUsed = false,

            TicketTypeId = ticketType6.Id,
            TicketType = ticketType6,

            CustomerId = customer1.Id,
            Customer = customer1,

            CreatedAt = createdAt,
            ModifiedAt = createdAt
        };

        context.Tickets.AddRange(
            ticket1,
            ticket2,
            ticket3,
            ticket4,
            ticket5,
            ticket6,
            ticket7,
            ticket8,
            ticket9,
            ticket10);

        context.SaveChanges();

        // ============================================================
        // EVENT SPONSORSHIPS
        // ============================================================

        var sponsorship1 = new EventSponsorship
        {
            EventId = event1.Id,
            Event = event1,

            SponsorId = sponsor1.Id,
            Sponsor = sponsor1,

            ContributionAmount = 15000m,
            PaymentStatus = PaymentStatus.Paid
        };

        var sponsorship2 = new EventSponsorship
        {
            EventId = event1.Id,
            Event = event1,

            SponsorId = sponsor2.Id,
            Sponsor = sponsor2,

            ContributionAmount = 8000m,
            PaymentStatus = PaymentStatus.Pending
        };

        var sponsorship3 = new EventSponsorship
        {
            EventId = event2.Id,
            Event = event2,

            SponsorId = sponsor1.Id,
            Sponsor = sponsor1,

            ContributionAmount = 12000m,
            PaymentStatus = PaymentStatus.Paid
        };

        var sponsorship4 = new EventSponsorship
        {
            EventId = event2.Id,
            Event = event2,

            SponsorId = sponsor3.Id,
            Sponsor = sponsor3,

            ContributionAmount = 10000m,
            PaymentStatus = PaymentStatus.Invoiced
        };

        var sponsorship5 = new EventSponsorship
        {
            EventId = event3.Id,
            Event = event3,

            SponsorId = sponsor2.Id,
            Sponsor = sponsor2,

            ContributionAmount = 7000m,
            PaymentStatus = PaymentStatus.Paid
        };

        context.EventSponsorships.AddRange(
            sponsorship1,
            sponsorship2,
            sponsorship3,
            sponsorship4,
            sponsorship5);

        context.SaveChanges();
    }
}