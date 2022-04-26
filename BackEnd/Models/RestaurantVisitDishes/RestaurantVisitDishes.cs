
namespace Hotelmanagement.BackEnd.Models.RestaurantVisitDishes
{
    public class RestaurantVisitDishes
    {
        public RestaurantVisitDishes(int restaurant_visit_id, int dish_id)
        {
            RestaurantVisitID = restaurant_visit_id;
            DishID = dish_id;
        }
    
        public int RestaurantVisitID { get; set; }
        public int DishID { get; set; }
    }
}