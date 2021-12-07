using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;

        [BindProperty(SupportsGet =true)]
        public Restaurant Restaurant { get; set; }

        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
            _restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();

            Restaurant = _restaurantData.GetRestaurantById(restaurantId);
            if (Restaurant is null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _restaurantData.Update(Restaurant);
            _restaurantData.Commit();
            return Page();
        }
    }
}