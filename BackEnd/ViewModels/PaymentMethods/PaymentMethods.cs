namespace Hotelmanagement.BackEnd.ViewModels.PaymentMethods
{
    public class PaymentMethods
    {
        public PaymentMethods(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        
    }
}