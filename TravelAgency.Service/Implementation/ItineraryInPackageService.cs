using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{

    public class ItineraryInPackageService : IItineraryInPackageService
    {
        private readonly IItineraryInPackageRepository repository;

        public ItineraryInPackageService(IItineraryInPackageRepository repository)
        {
            this.repository = repository;
        }

        public List<ItineraryInPackage> DeleteItinerary(Guid itineraryId)
        {
            List<ItineraryInPackage> iip = repository.GetItineraryInPackagesByItineraryId(itineraryId).ToList();
            return repository.DeleteItinerariesFromItineraryInPackage(iip).ToList();
        }

        public ItineraryInPackage CreateItineraryInPackage(ItineraryInPackage package)
        {
            return repository.Create(package);
        }

    }
}
