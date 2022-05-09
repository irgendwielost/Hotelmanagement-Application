namespace Hotelmanagement.BackEnd.ViewModels.VisitDiscount
{
    public class VisitDiscount
    {
        public VisitDiscount(int visit_id, int customer_discount_id, int special_offer_id, double value)
        {
            Visit_ID = visit_id;
            Customer_Discount_ID = customer_discount_id;
            Special_Offer_ID = special_offer_id;
            Value = value;
        }
        public int Visit_ID { get; set; }
        public int Customer_Discount_ID { get; set; }
        public int Special_Offer_ID { get; set; }
        
        public double Value { get; set; }
    }
}