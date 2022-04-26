using System;

namespace Hotelmanagement.BackEnd.Models.RestaurantVisitHotelGuest;

public class RestaurantVisitHotelGuest
{
    public RestaurantVisitHotelGuest(int restaurant_visit_id, int customer_id, DateTime visit_date)
    {
        RestaurantVisitID = restaurant_visit_id;
        CustomerID = customer_id;
        VisitDate = visit_date;
    }
    
    public int RestaurantVisitID { get; set; }
    public int CustomerID { get; set; }
    public DateTime VisitDate { get; set; }
    
}