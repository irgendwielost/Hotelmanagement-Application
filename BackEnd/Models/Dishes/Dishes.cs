using System;

namespace Hotelmanagement.BackEnd.Models.Dishes
{
    public class Dishes
    {
        public Dishes(int id, string designation, double price, int category_id, bool deleted, DateTime deleted_at)
        {
            ID = id;
            Designation = designation;
            Price = price;
            CategoryID = category_id;
            Deleted = deleted;
            DeletedAt = deleted_at;
        }
    
        public int ID { get; set; }
        public string Designation { get; set; }
        public double Price { get; set; }
        public int CategoryID { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}