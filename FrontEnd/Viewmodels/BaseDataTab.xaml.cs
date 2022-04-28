using System.Windows;
using System.Windows.Controls;
using Hotelmanagement.BackEnd.Models.Rooms;
using Hotelmanagement.FrontEnd.Viewmodels.Basedata;
using Hotelmanagement.FrontEnd.Viewmodels.Basedata.Hotel;
using Hotelmanagement.FrontEnd.Viewmodels.Basedata.Service;

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
        public RoomData _roomData = new RoomData();
        public ServiceData _serviceData = new ServiceData();
        public DiscountData _discountData = new DiscountData();
        public SpaData _spaData = new SpaData();
        public Admindata _admindata = new Admindata();
        //Switch to restaurantdata
        private void Restaurant(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _restaurantData;
        }
    
        //Switch to Roomdata
        private void Rooms(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _roomData;
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

        private void Testing(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }
    }
}