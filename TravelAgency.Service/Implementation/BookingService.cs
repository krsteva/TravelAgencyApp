using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Identity;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Implementation;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Bookings> repository;
        private readonly IUserRepository userRepository;
        private readonly ITravelPackagesService travelPackageService;
        private readonly UserManager<Customer> userManager;


        public BookingService(IRepository<Bookings> repository, IUserRepository userRepository, ITravelPackagesService travelPackageService, UserManager<Customer> customer)
        {
            this.repository = repository;
            this.userRepository = userRepository;
            this.travelPackageService = travelPackageService;
            this.userManager = userManager;
        }

        public Bookings Create(string loggedInUser, Bookings bookings)
        {

            TravelPackages tp = travelPackageService.GetPackage(bookings.TravelPackageId);
            bookings.TravelPackage = tp;
            bookings.TravelPackageId = tp.Id;
            var loggedInCustomer = userRepository.Get(loggedInUser);
            bookings.CustomerId = loggedInCustomer.Id;
            return repository.Insert(bookings);
        }

        public Bookings Delete(Guid Id)
        {
            Bookings booking = repository.Get(Id);
            return repository.Delete(booking);
        }

        public Bookings GetBookingById(Guid id)
        {
            return repository.Get(id);
        }

        public List<Bookings> GetCustomerBookings(string customerId)
        { 
            return repository.GetAll().Where(x=>x.CustomerId.Equals(customerId)).ToList();
        }

        public List<Bookings> insertAll(List<Bookings> bookings)
        {
            return repository.InsertMany(bookings);
        }

        public List<Bookings> ListAllBookings()
        {
            
            return repository.GetAll().ToList();
        }

        public Bookings Update(Bookings bookings)
        {
            return repository.Update(bookings);
        }
    }
}
