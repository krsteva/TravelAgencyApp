using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class ItineraryInPackage : BaseEntity
    {
        public Guid? ItineraryId { get; set; }
        public Guid? TravelPackageId { get; set; }
        public Itineraries? Itinerary { get; set; }
        public TravelPackages? TravelPackage { get; set; }
    }
}
