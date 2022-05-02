using System;
using System.ComponentModel.DataAnnotations;

namespace Hotelmanagement.BackEnd.Models.Customer
{
    public class Customer
    {
        public Customer(int id, string name,DateTime birthday ,string telephone, string email, string street,
            string place, string postalCode, bool deleted)
        {
            ID = id;
            Name = name;
            Birthday = birthday;
            Telephone = telephone;
            Email = email;
            Street = street;
            Place = place;
            PostalCode = postalCode;
            Deleted = deleted;
        }
        
        
        public int ID { get; set; }
        public string Name { get; set; }
        
        public DateTime Birthday { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string Place { get; set; }
        public string PostalCode { get; set; }
        public bool Deleted { get; set; }
    }
}