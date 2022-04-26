using System;

namespace Hotelmanagement.BackEnd.ViewModels.DrinkCategories
{
    public class DrinkCategories
    {
        public DrinkCategories(int id, string designation, bool alcoholic, bool under_twenty, bool deleted, DateTime deleted_at)
        {
            ID = id;
            Designation = designation;
            Alcoholic = alcoholic;
            Under_twenty = under_twenty;
            Deleted = deleted;
            Deleted_at = deleted_at;
        }
        
        public int ID { get; set; }
        public string Designation { get; set; }
        public bool Alcoholic { get; set; }
        public bool Under_twenty { get; set; }
        public bool Deleted { get; set; }
        public DateTime Deleted_at { get; set; }
    }
}