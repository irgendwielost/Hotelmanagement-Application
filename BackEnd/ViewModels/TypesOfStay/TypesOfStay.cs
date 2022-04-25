using System;

namespace Hotelmanagement.BackEnd.ViewModels.TypesOfStay
{
    public class TypesOfStay
    {
        public TypesOfStay(int id, string bezeichnung, double price_change, bool enfernt, DateTime entfernt_am)
        {
            ID = id;
            Bezeichnung = bezeichnung;
            Price_change = price_change;
            Enfernt = enfernt;
            Entfernt_Am = entfernt_am;
        }
        
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public double Price_change { get; set; }
        public bool Enfernt { get; set; }
        public DateTime Entfernt_Am { get; set; }
        
    }
}