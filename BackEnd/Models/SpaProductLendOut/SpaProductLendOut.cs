

namespace Hotelmanagement.BackEnd.Models.SpaProductLendOut
{
    public class SpaProductLendOut
    {
        public SpaProductLendOut(int spa_product_id, int customer_id)
        {
            SpaProductID = spa_product_id;
            CustomerID = customer_id;
        }
    
        public int SpaProductID { get; set; }
        public int CustomerID { get; set; }
    
    }
}