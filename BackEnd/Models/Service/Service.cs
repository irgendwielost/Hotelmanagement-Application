using System;
namespace Hotelmanagement.BackEnd.Models.Service
{
    public class Service
    {
        public Service(int id, string designation, string description, double price, int category_id,
            int duration_in_minutes, bool deleted, DateTime deleted_at )
        {
            ID = id;
            Designation = designation;
            Description = description;
            Price = price;
            Category_id = category_id;
            Duration_in_minutes = duration_in_minutes;
            Deleted = deleted;
            Deleted_at = deleted_at;
        }
    
        public int ID { get; set; }
        public string Designation { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Category_id { get; set; }
        public int Duration_in_minutes { get; set; }
        public bool Deleted { get; set; }
        public DateTime Deleted_at { get; set; }
    }
}
