using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Implementation;

namespace TravelAgency.Service.Interface
{
    public interface IItineraryInPackageService
    {
        List<ItineraryInPackage> DeleteItinerary(Guid id);

        ItineraryInPackage CreateItineraryInPackage(ItineraryInPackage package);
    }
}
