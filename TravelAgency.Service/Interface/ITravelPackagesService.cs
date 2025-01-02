using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.Service.Interface
{
    public interface ITravelPackagesService
    {
        public List<TravelPackages> ListAllPackages();
        public TravelPackages GetPackage (Guid iPackageId);

        public TravelPackages CreateNewPackage( TravelPackages travelPackages);
        public TravelPackages UpdatePackage(TravelPackages travelPackages);
        public TravelPackages DeletePackage(Guid iPackageId);




    }
}
