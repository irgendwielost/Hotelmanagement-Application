using System;

namespace Hotelmanagement.BackEnd.Models.Rooms;

public class Rooms
{
    public Rooms(int id, string designation, string size, double base_price, int amount, string description, 
        int extra_bed_capacity, double extra_bed_price, string situation, bool deleted, DateTime deleted_at)
    {
        ID = id;
        Designation = designation;
        Size = size;
        BasePrice = base_price;
        Amount = amount;
        Description = description;
        ExtraBedCapacity = extra_bed_capacity;
        ExtraBedPrice = extra_bed_price;
        Situation = situation;
        Deleted = deleted;
        DeletedAt = deleted_at;
        
    }
    
    public int ID { get; set; }
    public string Designation { get; set; }
    public string Size { get; set; }
    public double BasePrice { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; }
    public int ExtraBedCapacity { get; set; }
    public double ExtraBedPrice { get; set; }
    public string Situation { get; set; }
    public bool Deleted { get; set; }
    public DateTime DeletedAt { get; set; }
    
}