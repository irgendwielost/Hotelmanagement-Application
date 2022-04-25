using System;

namespace Hotelmanagement.BackEnd.ViewModels.SpecialOffers
{
    public class SpecialOffers
    {
        public SpecialOffers(int id, string bezeichnung, DateTime angebot_beginn, DateTime angebot_ende, int rabatt, bool entfernt, DateTime entfernt_am)
        {
            Id = id;
            Bezeichnung = bezeichnung;
            Angebot_Beginn = angebot_beginn;
            Angebot_Ende = angebot_ende;
            Rabatt = rabatt;
            Entfernt = entfernt;
            Entfernt_Am = entfernt_am;
        }
        
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public DateTime Angebot_Beginn { get; set; }
        public DateTime Angebot_Ende { get; set; }
        public int Rabatt { get; set; }
        public bool Entfernt { get; set; }
        public DateTime Entfernt_Am { get; set; }
    }
}