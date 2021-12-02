using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;
using System.Collections.Generic;

namespace OdeToFood.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public IEnumerable<Restaurant> restaurants { get; set; }
        public ListModel(IRestaurantData restaurant)
        {
            restaurantData = restaurant;
        }
        public void OnGet(string searchTerm)
        {          
            restaurants = restaurantData.GetRestaurantByName(searchTerm);
        }
    }
}
