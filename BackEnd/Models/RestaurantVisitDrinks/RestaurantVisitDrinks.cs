namespace Hotelmanagement.BackEnd.Models.RestaurantVisitDrinks;

public class RestaurantVisitDrinks
{
    public RestaurantVisitDrinks(int restaurant_visit_id, int drinks_id)
    {
        RestaurantVisitID = restaurant_visit_id;
        DrinksID = drinks_id;
    }
    
    public int RestaurantVisitID { get; set; }
    public int DrinksID { get; set; }
}