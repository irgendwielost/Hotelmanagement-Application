using System;

namespace Hotelmanagement.BackEnd.Models.Visit;

public class Visit
{
    public Visit(int id, int customer_id, int visit_type_of_stay_id, int person_amount, double service_costs,
        double room_costs, DateTime arrival, DateTime departure, double total_costs, bool complained, 
        string complain_reason, double dish_costs, bool customer_discount, bool special_offer)
    {
        ID = id;
        Customer_ID = customer_id;
        Visit_Type_Of_Stay_ID = visit_type_of_stay_id;
        Person_Amount = person_amount;
        Service_Costs = service_costs;
        Room_Costs = room_costs;
        Arrival = arrival;
        Departure = departure;
        Total_Costs = total_costs;
        Complained = complained;
        Complain_Reason = complain_reason;
        Dish_Costs = dish_costs;
        Customer_Discount = customer_discount;
        Special_Offer = special_offer;
    }
    
    public int ID { get; set; }
    public int Customer_ID { get; set; }
    public int Visit_Type_Of_Stay_ID { get; set; }
    public int Person_Amount { get; set; }
    public double Service_Costs { get; set; }
    public double Room_Costs { get; set; }
    public DateTime Arrival { get; set; }
    public DateTime Departure { get; set; }
    public double Total_Costs { get; set; }
    public bool Complained { get; set; }
    public string Complain_Reason { get; set; }
    public double Dish_Costs { get; set; }
    public bool Customer_Discount { get; set; }
    public bool Special_Offer { get; set; }
}