using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Enumeration;
using TravelAgency.Domain.Models;
using TravelAgency.Repository;
using TravelAgency.Repository.Implementation;
using TravelAgency.Service.Interface;

namespace TravelAgency.Web.Controllers
{
    
    public class ItinerariesController : Controller
    {
        private readonly IItinerariesSevice itinerariesSevice;
        private readonly ITravelPackagesService travelPackagesService;
        private readonly IItineraryInPackageService itineraryInPackage;

        public ItinerariesController(IItinerariesSevice itinerariesSevice, ITravelPackagesService travelPackagesService, IItineraryInPackageService travelInPackageService)
        {
            this.itinerariesSevice = itinerariesSevice;
            this.travelPackagesService = travelPackagesService;
            this.itineraryInPackage = travelInPackageService;
        }





        // GET: Itineraries
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(itinerariesSevice.ListAllItineraries());
        }

        // GET: Itineraries/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraries = itinerariesSevice.GetItinerariesById(id);
               
            if (itineraries == null)
            {
                return NotFound();
            }

            return View(itineraries);
        }

        // GET: Itineraries/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            List<SelectListItem> packages = travelPackagesService.ListAllPackages().Select(t => new SelectListItem
                       {
                           Text = t.Name,
                           Value = t.Id.ToString(),
                       }).ToList();

            ViewData["packages"] = packages;
            return View();
        }

        // POST: Itineraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public  IActionResult Create([Bind("Name,Description,DayNumber,Place,Image,ArivalTime,DepartutreTime,Id")] Itineraries itineraries, List<Guid> Packages)
        {
           
            if (ModelState.IsValid)
            {
                itineraries.Id = Guid.NewGuid();

                itineraryInPackage.DeleteItinerary(itineraries.Id);

                itinerariesSevice.CreateItinerary(itineraries);

                
                for (int i = 0; i< Packages.Count; i++)
                {
                    ItineraryInPackage iip = new ItineraryInPackage
                    {
                        Id = Guid.NewGuid(),
                        ItineraryId = itineraries.Id,
                        TravelPackageId = Packages[i]
                    };

                    itineraryInPackage.CreateItineraryInPackage(iip);
                }

                
                return RedirectToAction(nameof(Index));
            }

            return View(itineraries);
        }

        // GET: Itineraries/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraries = itinerariesSevice.GetItinerariesById(id);
            if (itineraries == null)
            {
                return NotFound();
            }



            return View(itineraries);
        }

        // POST: Itineraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("Name,Description,DayNumber,Place,Image,ArivalTime,DepartutreTime,Id")] Itineraries itineraries)
        {
            if (id != itineraries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    itinerariesSevice.UpdateItinerary(itineraries);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItinerariesExists(itineraries.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itineraries);
        }

        // GET: Itineraries/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraries = itinerariesSevice.GetItinerariesById(id);
            if (itineraries == null)
            {
                return NotFound();
            }

            return View(itineraries);
        }

        // POST: Itineraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            itinerariesSevice.DeleteItinerary(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ItinerariesExists(Guid id)
        {
            return itinerariesSevice.GetItinerariesById(id) != null;
        }
    }
}
