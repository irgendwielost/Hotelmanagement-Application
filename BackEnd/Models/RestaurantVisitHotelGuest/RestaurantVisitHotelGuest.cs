using System;

namespace Hotelmanagement.BackEnd.Models.RestaurantVisitHotelGuest
{
    public class RestaurantVisitHotelGuest
    {
        public RestaurantVisitHotelGuest(int restaurant_visit_id, int customer_id, int visit_id,DateTime visit_date)
        {
            RestaurantVisitID = restaurant_visit_id;
            CustomerID = customer_id;
            VisitDate = visit_date;
            VisitID = visit_id;            
        }
    
        public int RestaurantVisitID { get; set; }
        public int CustomerID { get; set; }
        
        public int VisitID { get; set; }
        public DateTime VisitDate { get; set; }
    
    }
}