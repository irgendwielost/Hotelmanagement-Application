namespace Hotelmanagement.BackEnd.Models.Salary;

public class Salary
{
    public Salary(int id, string area)
    {
        ID = id;
        Area = area;
    }
    
    public int ID { get; set; }
    public string Area { get; set; }
}