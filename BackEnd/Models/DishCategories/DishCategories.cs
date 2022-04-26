using System;

namespace Hotelmanagement.BackEnd.Models.DishCategories
{
    public class DishCategories
    {
        public DishCategories(int id, string designation, bool deleted, DateTime deleted_at)
        {
            ID = id;
            Designation = designation;
            Deleted = deleted;
            Deleted_at = deleted_at;
        }
    
        public int ID { get; set; }
        public string Designation { get; set; }
        public bool Deleted { get; set; }
        public DateTime Deleted_at { get; set; }
    }
}