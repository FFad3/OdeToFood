using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext _context)
        {
            this._context = _context;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetRestaurantById(id);
            if (restaurant is not null)
            {
                _context.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return _context.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantByName(string name)
        {
            var query = from r in _context.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        select r;

            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = _context.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;

            return updatedRestaurant;
        }
    }
}
