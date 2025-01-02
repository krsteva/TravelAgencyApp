using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelAgency.Domain.Models;
using TravelAgency.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgency.Web.Pages;
using Microsoft.AspNetCore.Mvc;

public class IndexModel : PageModel
{
    private readonly ITravelPackagesService _travelPackageService;

    public IndexModel(ITravelPackagesService travelPackageService)
    {
        _travelPackageService = travelPackageService;
    }

    public IEnumerable<TravelPackages> FeaturedPackages { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SearchTerm { get; set; }

    public async Task OnGet()
    {
        var packages =  _travelPackageService.ListAllPackages();

        if (!string.IsNullOrWhiteSpace(SearchTerm))
        {
            FeaturedPackages = packages.Where(p => p.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase));
        }
        else
        {
            FeaturedPackages = packages; 
        }

    }
}
