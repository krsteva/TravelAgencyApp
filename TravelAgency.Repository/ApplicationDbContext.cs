using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Identity;
using TravelAgency.Domain.Models;

namespace TravelAgency.Repository
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<TravelPackages> TravelPackages { get; set; }
        public virtual DbSet<Itineraries> Itineraries { get; set;}
        public virtual DbSet<Bookings> Bookings { get; set;}
        public virtual DbSet<ItineraryInPackage> ItineraryInPackages { get; set; }



    }
}
