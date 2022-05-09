namespace Hotelmanagement.BackEnd.Models.Tax;

public class Tax
{
    public Tax(int id, string designation, double value)
    {
        Id = id;
        Designation = designation;
        Value = value;
    }
    
    public int Id { get; set; }
    public string Designation { get; set; }
    public double Value { get; set; }
}