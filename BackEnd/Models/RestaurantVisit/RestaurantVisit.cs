namespace Hotelmanagement.BackEnd.Models.RestaurantVisit
{
    public class RestaurantVisit
    {
        public RestaurantVisit(int id, int table_id, double total_costs, bool hotel_guest)
        {
            ID = id;
            TableID = table_id;
            TotalCosts = total_costs;
            HotelGuest = hotel_guest;
        }
    
        public int ID { get; set; }
        public int TableID { get; set; }
        public double TotalCosts { get; set; }
        public bool HotelGuest { get; set; }
    }
}