using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesContacts.Data;
using SalesContacts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SalesContacts.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IContext context;

        public IndexModel(IContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public int? GenderId { get; set; }

        [BindProperty]
        public int? CityId { get; set; }

        [BindProperty]
        public int? RegionId { get; set; }

        [BindProperty]
        public DateTime? LastPurchaseStart { get; set; }

        [BindProperty]
        public DateTime? LastPurchaseEnd { get; set; }

        [BindProperty]
        public int? ClassificationId { get; set; }

        [BindProperty]
        public int? SellerId { get; set; }

        [BindProperty]
        public bool IsAdministrator { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        public IEnumerable<SelectListItem> Regions { get; set; }

        public IEnumerable<SelectListItem> Classifications { get; set; }

        public IEnumerable<SelectListItem> Sellers { get; set; }

        public IEnumerable<SearchResultVm> Results { get; set; }

        public IActionResult OnGet()
        {
            LoadCriterias();
            this.Results = Search();

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            LoadCriterias();
            this.Results = Search();

            return Page();
        }

        private void LoadCriterias()
        {
            this.Genders = GetGenders();
            this.Cities = GetCities();
            this.Regions = GetRegions();
            this.Classifications = GetClassifications();
            this.Sellers = GetSellers();
            this.IsAdministrator = this.User.IsInRole("Administrator");
        }

        private IEnumerable<SelectListItem> GetGenders()
        {
            return
                context.Genders
                .Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString() })
                .ToList()
                .Union(new List<SelectListItem> { new SelectListItem { Text = "Select an item", Value = string.Empty } })
                .OrderBy(o => o.Value);
        }

        private IEnumerable<SelectListItem> GetCities()
        {
            return
                context.Cities
                .Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString() })
                .ToList()
                .Union(new List<SelectListItem> { new SelectListItem { Text = "Select an item", Value = string.Empty } })
                .OrderBy(o => o.Value);
        }

        private IEnumerable<SelectListItem> GetRegions()
        {
            return
                context.Regions
                .Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString() })
                .ToList()
                .Union(new List<SelectListItem> { new SelectListItem { Text = "Select an item", Value = string.Empty } })
                .OrderBy(o => o.Value);
        }

        private IEnumerable<SelectListItem> GetClassifications()
        {
            return
                context.Classifications
                .Select(g => new SelectListItem { Text = g.Name, Value = g.Id.ToString() })
                .ToList()
                .Union(new List<SelectListItem> { new SelectListItem { Text = "Select an item", Value = string.Empty } })
                .OrderBy(o => o.Value);
        }

        private IEnumerable<SelectListItem> GetSellers()
        {
            return
                context.Users
                .Include(u => u.UserRole)
                .Where(u => u.UserRole.Name == "Seller")
                .Select(g => new SelectListItem { Text = g.Login, Value = g.Id.ToString() })
                .ToList()
                .Union(new List<SelectListItem> { new SelectListItem { Text = "Select an item", Value = string.Empty } })
                .OrderBy(o => o.Value);
        }

        private IEnumerable<SearchResultVm> Search()
        {
            var query = this.context.Customers
                .Include(i => i.Gender)
                .Include(i => i.City)
                .Include(i => i.Region)
                .Include(i => i.Classification)
                .Include(i => i.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(this.Name))
            {
                query = query.Where(q => q.Name.Contains(this.Name));
            }

            if (this.GenderId.HasValue)
            {
                query = query.Where(q => q.GenderId == this.GenderId.Value);
            }

            if (this.CityId.HasValue)
            {
                query = query.Where(q => q.CityId == this.CityId.Value);
            }

            if (this.RegionId.HasValue)
            {
                query = query.Where(q => q.RegionId == this.RegionId.Value);
            }

            if (this.LastPurchaseStart.HasValue)
            {
                query = query.Where(q => q.LastPurchase >= LastPurchaseStart.Value);
            }

            if (this.LastPurchaseEnd.HasValue)
            {
                query = query.Where(q => q.LastPurchase <= LastPurchaseEnd.Value);
            }

            if (this.ClassificationId.HasValue)
            {
                query = query.Where(q => q.ClassificationId == this.ClassificationId.Value);
            }

            if (this.SellerId.HasValue)
            {
                query = query.Where(q => q.UserId == this.SellerId.Value);
            }

            if (!this.IsAdministrator)
            {
                var sellerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                query = query.Where(q => q.UserId == sellerId);
            }

            var results = query
                .ToList();

            var viewModels = results
                .Select(r => new SearchResultVm
                {
                    Classification = r.Classification.Name,
                    Name = r.Name,
                    Phone = r.Phone,
                    Gender = r.Gender.Name,
                    City = r.City.Name,
                    Region = r.Region.Name,
                    LastPurchase = r.LastPurchase.ToString("dd/MM/yyyy"),
                    Seller = r.User.Login
                })
                .ToList();

            return viewModels;
        }
    }
}
