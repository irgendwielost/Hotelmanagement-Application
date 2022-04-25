using System;

namespace Hotelmanagement.BackEnd.ViewModels.Drinks
{
    public class Drinks
    {
        public Drinks(int id, string designation,int category_id,double price, bool entfernt, DateTime entfernt_am)
        {
            Id = id;
            Designation = designation;
            Category_ID = category_id;
            Price = price;
            Entfernt = entfernt;
            Entfernt_Am = entfernt_am;
        }
        
        public int Id { get; set; }
        public string Designation { get; set; }
        
        public int Category_ID { get; set; }
        public double Price { get; set; }
        public bool Entfernt { get; set; }
        public DateTime Entfernt_Am { get; set; }
    }
}