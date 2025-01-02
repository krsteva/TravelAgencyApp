using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Identity;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class ItineraryInPackageRepository : IItineraryInPackageRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<ItineraryInPackage> entities;

        public ItineraryInPackageRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ItineraryInPackage Create(ItineraryInPackage package)
        {
            entities.Add(package);
            context.SaveChanges();
            return package;
        }

        public ICollection<ItineraryInPackage> DeleteItinerariesFromItineraryInPackage(List<ItineraryInPackage> itineraryInPackages)
        {

            if (entities == null)
            {
                entities = context.Set<ItineraryInPackage>();
            }

            try
            {
                entities.RemoveRange(itineraryInPackages);

                context.SaveChanges();

                return itineraryInPackages;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting itineraries: {ex.Message}");
                throw;
            }

        }

        public ICollection<ItineraryInPackage> GetItineraryInPackagesByItineraryId(Guid id)
        {

            if (entities == null)
            {
                entities = context.Set<ItineraryInPackage>();
            }

            return entities
                .Where(iip => iip.ItineraryId == id)
                .Include(iip => iip.Itinerary)
                .Include(iip => iip.TravelPackage) 
                .ToList();

        }
    }
}
