using System.Windows;
using System.Windows.Controls;
using Hotelmanagement.FrontEnd.Viewmodels.Basedata;

namespace Hotelmanagement.FrontEnd.Viewmodels
{
    public partial class BaseDataTab : UserControl
    {
        public BaseDataTab()
        {
            InitializeComponent();
        }
    
        //Contents of the tab
        public RestaurantData _restaurantData = new RestaurantData();
        public Hoteldata _hoteldata = new Hoteldata();
        public ServiceData _serviceData = new ServiceData();
        public DiscountData _discountData = new DiscountData();
        public SpaData _spaData = new SpaData();
        public Admindata _admindata = new Admindata();
        //Switch to restaurantdata
        private void Restaurant(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _restaurantData;
        }
    
        //Switch to HotelData
        private void Hotel(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _hoteldata;
        }
    
        //Switch to ServiceData
        private void Service(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _serviceData;
        }
    
        //Switch to DiscountData
        private void Discount(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _discountData;
        }
    
        //Switch to SpaData
        private void Spa(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _spaData;
        }
    
        private void Admin (object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _admindata;
        }

        private void HideStartUp()
        {
        
        }
    }
}