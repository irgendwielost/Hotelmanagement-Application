using System.Windows;
using System.Windows.Controls;
using Hotelmanagement.BackEnd.Database;

namespace Hotelmanagement.FrontEnd.Viewmodels
{
    public partial class Main : UserControl
    {
        public Main()
        {
            InitializeComponent();
        }

        public MainWindow mainWindow;
        public Database database;
        public EmployeeTab _employeeTab = new EmployeeTab();
        public RestaurantTab _restaurantTab = new RestaurantTab();
        public CustomerTab _customerTab = new CustomerTab();
        public SpaTab _spaTab = new SpaTab();
        public BaseDataTab _baseDataTab = new BaseDataTab();
        public BookingTab _bookingTab = new BookingTab();
    
        //Contentcontrols
        private void Customer(object sender, RoutedEventArgs e)
        {
            mainWindow.ContentControl.Content = _customerTab;
        }
  
        private void Restaurant(object sender, RoutedEventArgs e)
        {
            mainWindow.ContentControl.Content = _restaurantTab;
        }
    
        private void DbTest(object sender, RoutedEventArgs e)
        {
        }
    
        private void Spa(object sender, RoutedEventArgs e)
        {
            mainWindow.ContentControl.Content = _spaTab;
        }
    
        private void BaseData(object sender, RoutedEventArgs e)
        {
            mainWindow.ContentControl.Content = _baseDataTab;
        }
    
        private void Booking(object sender, RoutedEventArgs e)
        {
            mainWindow.ContentControl.Content = _bookingTab;
        }
    
    }
}