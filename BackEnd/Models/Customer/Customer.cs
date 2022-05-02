using System;
using System.ComponentModel.DataAnnotations;

namespace Hotelmanagement.BackEnd.Models.Customer
{
    public class Customer
    {
        public Customer(int id, string name, string telefon, string email, string strasse,
            string wohnort, string plz, bool entfernt)
        {
            ID = id;
            Name = name;
            Telefon = telefon;
            Email = email;
            Strasse = strasse;
            Wohnort = wohnort;
            Plz = plz;
            Entfernt = entfernt;
        }
        
        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Strasse { get; set; }
        public string Wohnort { get; set; }
        public string Plz { get; set; }
        public bool Entfernt { get; set; }
    }
}