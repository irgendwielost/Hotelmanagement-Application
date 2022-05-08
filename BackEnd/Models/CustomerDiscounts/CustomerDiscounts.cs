namespace Hotelmanagement.BackEnd.Models.CustomerDiscounts
{
    public class CustomerDiscounts
    {
        public CustomerDiscounts(int id, int customerId, string designation,double discount_value)
        {
            ID = id;
            Designation = designation;
            CustomerID = customerId;
            DiscountValue = discount_value;
        }
    
        public int ID { get; set; }
        
        public string Designation { get; set; }
        public int CustomerID { get; set; }
        public double DiscountValue { get; set; }
    
    }
}