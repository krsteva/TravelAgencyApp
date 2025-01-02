using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.Repository.Interface
{
    public interface IItineraryInPackageRepository
    {
        ICollection<ItineraryInPackage> GetItineraryInPackagesByItineraryId(Guid id);
        ICollection<ItineraryInPackage> DeleteItinerariesFromItineraryInPackage(List<ItineraryInPackage> itineraryInPackages);
        ItineraryInPackage Create(ItineraryInPackage package);
    }
}
