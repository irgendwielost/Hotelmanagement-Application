using System;

namespace Hotelmanagement.BackEnd.Models.Invoice
{
    public class Invoice
    {
        public Invoice(int id, int visit_id, double visit_total_costs, int customer_discount_id, int specialoffers_id,
            bool paid, bool deleted, DateTime deleted_at)
        {
            ID = id;
            VisitID = visit_id;
            VisitTotalCosts = visit_total_costs;
            Customer_discountID = customer_discount_id;
            SpecialoffersID = specialoffers_id;
            Paid = paid;
            Deleted = deleted;
            DeletedAt = deleted_at;
        }
    
        public int ID { get; set; }
        public int VisitID { get; set; }
        public double VisitTotalCosts { get; set; }
        public int Customer_discountID { get; set; }
        public int SpecialoffersID { get; set; }
        public bool Paid { get; set; }
        public bool Deleted { get; set; }
        public DateTime DeletedAt { get; set; }
    
    }
}