namespace Hotelmanagement.BackEnd.Models.CustomerDiscounts
{
    public class CustomerDiscounts
    {
        public CustomerDiscounts(int id, int customerId, int discount_value)
        {
            ID = id;
            CustomerID = customerId;
            DiscountValue = discount_value;
        }
    
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int DiscountValue { get; set; }
    
    }
}