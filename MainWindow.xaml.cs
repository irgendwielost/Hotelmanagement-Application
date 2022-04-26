using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hotelmanagement.FrontEnd.Viewmodels;
using Hotelmanagement.FrontEnd.Viewmodels.Basedata;
using MaterialDesignExtensions.Controls;
namespace Hotelmanagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private EmployeeTab _employeeTab = new EmployeeTab();
        private Main _main = new Main();
        private Transparent _transparent = new Transparent();
        private RestaurantTab _restaurantTab = new RestaurantTab();
        private CustomerTab _customerTab = new CustomerTab();
        private SpaTab _spaTab = new SpaTab();
        private DepartmentTab _departmentTab = new DepartmentTab();
        private BaseDataTab _baseDataTab = new BaseDataTab();
        private BookingTab _bookingTab = new BookingTab();
        
        //*****Tab Control*****
        
        //Main
        private void Switch_Main(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _transparent;
            ShowMain();
            Change_Tab(0);
        }
        
        //Employee
        private void Switch_Employee(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _employeeTab;
            HideMain();
        }
        
        //Restaurant
        private void Switch_Restaurant(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _restaurantTab;
            HideMain();
            Change_Tab(3);
        }
        
        //Customer
        public void Switch_Customer(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _customerTab;
            HideMain();
            Change_Tab(2);
        }
        
        //Spa
        private void Switch_Spa(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _spaTab;
            HideMain();
            Change_Tab(4);
        }
        //BaseData
        private void Switch_BaseData(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _baseDataTab;
            HideMain();
            Change_Tab(5);
        }
        
        //Booking
        private void Switch_Booking(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _bookingTab;
            HideMain();
            Change_Tab(1);
        }

        private void HideMain()
        {
            Maincontrol.Visibility = Visibility.Hidden;
        }

        private void ShowMain()
        {
            Maincontrol.Visibility = Visibility.Visible;
        }
        
        private void Change_Tab(int tabIndex)
        {
            TabControl.SelectedIndex = tabIndex; 
        }
    }
}