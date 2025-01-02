using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Enumeration;

namespace TravelAgency.Domain.Models
{
    public class TravelPackages : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        public string Duration { get; set; }
        public int Seats { get; set; }
        public double Price { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set;}
        public Category Category { get; set; }
        public virtual ICollection<Bookings>? Bookings { get; set; }
        public virtual ICollection<ItineraryInPackage>? ItineraryInPackages { get; set; }

    }
}
