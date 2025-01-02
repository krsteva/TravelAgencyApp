using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using ExcelDataReader;
using GemBox.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Stripe;
using TravelAgency.Domain.Enumeration;
using TravelAgency.Domain.Identity;
using TravelAgency.Domain.Models;
using TravelAgency.Domain.Models.Payment;
using TravelAgency.Repository;
using TravelAgency.Repository.Implementation;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;
using TravelAgency.Web.Data.Migrations;

namespace TravelAgency.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingService bookingService;
        private readonly ITravelPackagesService travelPackagesService;
        private readonly IUserRepository _userRepository;
        private readonly StripeSettings _stripeSettings;
        private readonly UserManager<TravelAgency.Domain.Identity.Customer> _userManager;

        public BookingsController(
            UserManager<TravelAgency.Domain.Identity.Customer> userManager,
            IBookingService bookingService,
            ITravelPackagesService travelPackagesService,
            IUserRepository userRepository,
            IOptions<StripeSettings> stripeSettings)
        {
            this.bookingService = bookingService;
            this.travelPackagesService = travelPackagesService;
            this._userRepository = userRepository;
            this._stripeSettings = stripeSettings.Value;
            this._userManager = userManager;
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }



        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            ViewData["status"] = GetStatusSelectList();

            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            if (loggedInUser.Equals(""))
            {
                return View("Error");
                //todo No Access page
            }

            TravelAgency.Domain.Identity.Customer customer = _userRepository.Get(loggedInUser);

            List<Bookings> bookings = new List<Bookings>();

            if (await _userManager.IsInRoleAsync(customer, "Admin"))
            {
                bookings = bookingService.ListAllBookings();
            }
            else
            {
                bookings = bookingService.GetCustomerBookings(loggedInUser);
            }

            return View(bookings);
        }


        // GET: Bookings/Details/5
        public IActionResult Details(Guid id)
        {

            var bookings = bookingService.GetBookingById(id);

            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ImportBookings(IFormFile file)
        {
            var a = Directory.GetCurrentDirectory();
            string pathToUpload = $"{Directory.GetCurrentDirectory()}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(pathToUpload))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            List<Bookings> bookigns = getAllBookingFromFile(file.FileName);
            bookingService.insertAll(bookigns);

            return RedirectToAction("Index");
        }
        private List<Bookings> getAllBookingFromFile(string fileName)
        {
            List<Bookings> bookings = new List<Bookings>();
            string filePath = $"{Directory.GetCurrentDirectory()}\\files\\{fileName}";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            Domain.Identity.Customer admin = _userRepository.Get(loggedInUser);
            using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        bookings.Add(new Bookings
                        {
                            Name = reader.GetValue(0).ToString(),
                            LastName = reader.GetValue(1).ToString(),
                            Passport = reader.GetValue(2).ToString(),
                            Customer = admin,   
                            DateBooked = DateOnly.ParseExact(reader.GetValue(3).ToString(), "dd.MM.yyyy hh:mm:ss", CultureInfo.InvariantCulture),
                            Status = Status.CONFIRMED,
                            TravelPackageId = Guid.Parse(reader.GetValue(4).ToString()),
                            Id = Guid.NewGuid()
                        });
                    }
                }
            }
            return bookings;
        }

        // GET: Bookings/Create
        public IActionResult Create(Guid travelPackageId)
        {

            ViewData["status"] = GetStatusSelectList();
            ViewData["travelPackageId"] = travelPackageId;



            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,LastName,Passport,TravelPackageId")] Bookings bookings)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            bookings.DateBooked = DateOnly.FromDateTime(DateTime.Now);
            bookings.Status = Status.PENDING;
            bookingService.Create(loggedInUser, bookings);
            return RedirectToAction("Index", "Bookings");
        }



        // GET: Bookings/Edit/5
        public IActionResult Edit(Guid id)
        {


            var bookings = bookingService.GetBookingById(id);
            if (bookings == null)
            {
                return NotFound();
            }
            ViewData["status"] = GetStatusSelectList();
            ViewData["thisStatus"] = bookings.Status;
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,LastName,Passport,Id")] Bookings bookings)
        {
            if (id != bookings.Id)
            {
                return NotFound();
            }

            var booking = bookingService.GetBookingById(id);
            booking.Name = bookings.Name;
            booking.LastName = bookings.LastName;
            booking.Passport = bookings.Passport;
            
            bookingService.Update(booking);   
             
            return RedirectToAction("Index");
        }

        // GET: Bookings/Delete/5
        public IActionResult Delete(Guid id)
        {


            var bookings = bookingService.GetBookingById(id);
            bookings.Status = Status.CANCELED;
            bookingService.Update(bookings);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {

            //bookingService.Delete(id);


            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(Guid id)
        {
            return bookingService.GetBookingById(id) != null;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PayOrder(string stripeEmail, string stripeToken, Guid id)
        {
            try
            {
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
                var customerService = new CustomerService();
                var chargeService = new ChargeService();

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

                Bookings booking = bookingService.GetBookingById(id);

                var customer = customerService.Create(new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    Source = stripeToken
                });

                var charge = chargeService.Create(new ChargeCreateOptions
                {
                    Amount = (Convert.ToInt32(booking.TravelPackage.Price * 100)), // Stripe expects amount in cents
                    Description = "TravelAgency Application Payment",
                    Currency = "usd",
                    Customer = customer.Id
                });

                if (charge.Status == "succeeded")
                {
                    booking.Status = Status.CONFIRMED;
                    booking.TravelPackage.Seats--;
                    travelPackagesService.UpdatePackage(booking.TravelPackage);
                    bookingService.Update(booking);
                    return RedirectToAction("SuccessPayment");
                }

                return RedirectToAction("NotsuccessPayment");
            }
            catch (StripeException ex)
            {
                // Handle specific Stripe errors
                return RedirectToAction("NotsuccessPayment");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                return RedirectToAction("NotsuccessPayment");
            }
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult NotsuccessPayment()
        {
            return View();
        }



        private List<SelectListItem> GetStatusSelectList()
        {
            return Enum.GetValues(typeof(Status))
                       .Cast<Status>()
                       .Select(c => new SelectListItem
                       {
                           Text = c.ToString(),
                           Value = c.ToString()
                       }).ToList();
        }


        public FileContentResult CreateInvoice(Guid id)
        {
            var customer = bookingService.GetBookingById(id);

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "BookingDetails.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{Name}}", customer.Name);
            document.Content.Replace("{{LastName}}", customer.LastName);
            document.Content.Replace("{{Passport}}", customer.Passport);
            document.Content.Replace("{{DateBooked}}", customer.DateBooked.ToString());
            document.Content.Replace("{{Price}}", customer.TravelPackage.Price.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new GemBox.Document.PdfSaveOptions());

            return File(stream.ToArray(), new GemBox.Document.PdfSaveOptions().ContentType, "BookingDetails.pdf");

        }



    }
}
