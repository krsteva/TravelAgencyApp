using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelAgency.Domain.Enumeration;
using TravelAgency.Domain.Models;
using TravelAgency.Service.Interface;

namespace TravelAgency.Web.Controllers
{
    public class TravelPackagesController : Controller
    {
        private readonly ITravelPackagesService packagesService;

        public TravelPackagesController(ITravelPackagesService packagesService)
        {
            this.packagesService = packagesService;
        }

        // GET: TravelPackages
        public IActionResult Index()
        {
            return View(packagesService.ListAllPackages());
        }
        
        // GET: TravelPackages/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            TravelPackages? travelPackage = packagesService?.GetPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // GET: TravelPackages/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["categories"] = GetCategoriesSelectList();

            return View();
        }

        // POST: TravelPackages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Name, Description, Image, Duration, Seats, Price, StartDate, EndDate, Category")] TravelPackages package)
        {
            if (ModelState.IsValid)
            {
                packagesService.CreateNewPackage(package);
                return RedirectToAction(nameof(Index));
            }

            ViewData["categories"] = GetCategoriesSelectList(); 
            return View(package);
        }

        // GET: TravelPackages/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var travelPackage = packagesService.GetPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            ViewData["categories"] = GetCategoriesSelectList(); // Populate dropdown with pre-selected category
            ViewData["category"] = travelPackage.Category;
            return View(travelPackage);
        }

        // POST: TravelPackages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, [Bind("Id, Name, Description, Image, Duration, Seats, Price, StartDate, EndDate, Category")] TravelPackages package)
        {
            if (id != package.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                packagesService.UpdatePackage(package);
                return RedirectToAction(nameof(Index));
            }

            ViewData["categories"] = GetCategoriesSelectList(); // Repopulate dropdown on error
            return View(package);
        }

        // GET: TravelPackages/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var travelPackage = packagesService.GetPackage(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(Guid id)
        {
            packagesService.DeletePackage(id);
            return RedirectToAction(nameof(Index));
        }

        // Helper method to get categories as SelectList
        private List<SelectListItem> GetCategoriesSelectList()
        {
            return Enum.GetValues(typeof(Category))
                       .Cast<Category>()
                       .Select(c => new SelectListItem
                       {
                           Text = c.ToString(),
                           Value = c.ToString()
                       }).ToList();
        }
    }
}
