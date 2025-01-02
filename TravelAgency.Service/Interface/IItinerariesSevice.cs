using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.Service.Interface
{

    public interface IItinerariesSevice
    {
        public List<Itineraries> ListAllItineraries();
        public Itineraries GetItinerariesById(Guid Id);

        public Itineraries CreateItinerary(Itineraries itineraries);
        public Itineraries UpdateItinerary(Itineraries itineraries);
        public Itineraries DeleteItinerary(Guid Id);
    }
}
