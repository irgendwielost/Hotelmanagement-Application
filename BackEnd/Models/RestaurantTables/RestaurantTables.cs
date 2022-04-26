using System;

namespace Hotelmanagement.BackEnd.Models.RestaurantTables
{
    public class RestaurantTables
    {
        public RestaurantTables(int id, int seat_capacity, bool is_occupied, bool deleted,DateTime deleted_at)
        {
            ID = id;
            SeatCapacity = seat_capacity;
            IsOccupied = is_occupied;
            Deleted = deleted;
            DeletedAt = deleted_at;
        }
    
        public int ID { get; set; }
        public int SeatCapacity { get; set; }
        public bool IsOccupied { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletedAt { get; set; }
    
    }
}