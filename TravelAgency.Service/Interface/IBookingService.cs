using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.Service.Interface
{
    public interface IBookingService
    {
        public List<Bookings> ListAllBookings();
        public List<Bookings> GetCustomerBookings(string customerId);
        public Bookings GetBookingById(Guid id);
        public Bookings Create(string loggedInUser ,Bookings bookings);
        public Bookings Update(Bookings bookings);
        public Bookings Delete(Guid Id);

        public List<Bookings> insertAll(List<Bookings> bookings);

     




    }
}
