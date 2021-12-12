using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class DeleteModel : PageModel
    {
        private readonly IRestaurantData _context;

        public Restaurant Restaurant { get; set; }

        public DeleteModel(IRestaurantData _context)
        {
            this._context = _context;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = _context.GetRestaurantById(restaurantId);
            if (Restaurant is null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost(int restaurantId)
        {
            var restaurant = _context.Delete(restaurantId);
            _context.Commit();

            if (restaurant is null)
            {
                return RedirectToPage("./NotFound");
            }
            TempData["Message"] = $"{restaurant.Name} deleted";
            return RedirectToPage("./List");
        }
    }
}
