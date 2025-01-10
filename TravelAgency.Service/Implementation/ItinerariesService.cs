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
   

  

    public class ItinerariesService : IItinerariesSevice
   
    {
        private readonly IRepository<Itineraries> repository;
        private readonly IItineraryInPackageService itineraryInPackageService;

        public ItinerariesService(IRepository<Itineraries> repository, IItineraryInPackageService itineraryInPackageService)
        {
            this.repository = repository;
            this.itineraryInPackageService = itineraryInPackageService;
        }

        public Itineraries CreateItinerary(Itineraries itineraries)
        {
           return repository.Insert(itineraries);   
        }

        public Itineraries DeleteItinerary(Guid Id)
        {
            Itineraries itinerary = repository.Get(Id);
            itineraryInPackageService.DeleteItinerary(Id);
            return repository.Delete(itinerary);
        }

        public Itineraries GetItinerariesById(Guid Id)
        {
            return repository.Get(Id);
        }

        public List<Itineraries> ListAllItineraries()
        {
            return repository.GetAll().ToList();
        }

        public Itineraries UpdateItinerary(Itineraries itineraries)
        {
            return repository.Update(itineraries);
        }
    }
}
