using System;

namespace Hotelmanagement.BackEnd.ViewModels.CustomerPaymentmethods
{
    public class CustomerPaymentmethods
    {
        public CustomerPaymentmethods(int id, int customer_id, int paymentmethod_id, string method_information, bool deleted )
        {
            ID = id;
            Customer_ID = customer_id;
            Paymentmethod_ID = paymentmethod_id;
            Method_Information = method_information;
            Deleted = deleted;
        }
        
        public int ID { get; set; }
        public int Customer_ID { get; set; }
        public int Paymentmethod_ID { get; set; }
        public string Method_Information { get; set; }
        public bool Deleted { get; set; }
        
    }
}