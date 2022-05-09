using System;

namespace Hotelmanagement.BackEnd.Models.Invoice
{
    public class Invoice
    {
        public Invoice(int id, int visit_id, double visit_total_costs,double room_costs, double service_costs, 
            double restaurant_costs, double customer_discount_value, double specialoffers_value, int tax_id, double tax_sum,
            bool paid, bool deleted)
        {
            ID = id;
            VisitID = visit_id;
            VisitTotalCosts = visit_total_costs;
            Room_Costs = room_costs;
            Service_Costs = service_costs;
            Restaurant_Costs = restaurant_costs;
            Customer_Discount_Value = customer_discount_value;
            Specialoffers_Value = specialoffers_value;
            Tax_Id = tax_id;
            Tax_Sum = tax_id;
            Paid = paid;
            Deleted = deleted;
        }
    
        public int ID { get; set; }
        public int VisitID { get; set; }
        public double VisitTotalCosts { get; set; }
        public double Room_Costs { get; set; }
        public double Service_Costs { get; set; }
        public double Restaurant_Costs { get; set; }
        public double Customer_Discount_Value { get; set; }
        public double Specialoffers_Value { get; set; }
        public int Tax_Id { get; set; }
        public double Tax_Sum { get; set; }
        public bool Paid { get; set; }
        public bool Deleted { get; set; }
    
    }
}