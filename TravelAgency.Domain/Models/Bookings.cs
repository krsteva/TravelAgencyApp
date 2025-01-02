using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Enumeration;
using TravelAgency.Domain.Identity;

namespace TravelAgency.Domain.Models
{
    public class Bookings : BaseEntity
    {
       
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Status Status { get; set; }
        public double PricePaid { get; set; }
        public string Passport { get; set; }
        public DateOnly DateBooked { get; set; }

        public Guid TravelPackageId {  get; set; }

        public TravelPackages TravelPackage { get; set; }
    }
}
