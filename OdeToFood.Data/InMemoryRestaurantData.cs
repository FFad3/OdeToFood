using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData :IRestaurantData
    {
        List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant(){ Id=1,Name="Scott's Pizza",Location="Maryland",Cuisine=CuisineType.Italian},
                new Restaurant(){ Id=2,Name="U Szkota",Location="Gdańsk",Cuisine=CuisineType.Mexican},
                new Restaurant(){ Id=3,Name="Kombajn",Location="Warszawa",Cuisine=CuisineType.Indian}
            };
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(x => x.Id)+1;
            return newRestaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(x => x.Id == id);
            if (restaurant is not null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(x=>x.Id == updatedRestaurant.Id);
            if (restaurant is not null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }
    }
}
