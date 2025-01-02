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
    public class TravelPackageService : ITravelPackagesService
    {
        private readonly IRepository<TravelPackages> repository;

        public TravelPackageService(IRepository<TravelPackages> repository)
        {
            this.repository = repository;
        }

        public TravelPackages CreateNewPackage(TravelPackages travelPackages)
        {
            return repository.Insert(travelPackages);
        }

        public TravelPackages DeletePackage(Guid packageId)
        {
            var product_to_delete = this.GetPackage(packageId);
            return repository.Delete(product_to_delete);

        }

       

        public TravelPackages GetPackage(Guid PackageId)
        {
            return repository.Get(PackageId);
        }

        public List<TravelPackages> ListAllPackages()
        {

            return repository.GetAll().ToList();
        }

        public TravelPackages UpdatePackage(TravelPackages travelPackages)
        {
            return repository.Update(travelPackages);
        }
    }
}
