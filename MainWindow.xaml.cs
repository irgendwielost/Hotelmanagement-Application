using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using Hotelmanagement.BackEnd.Models.Customer;
using Hotelmanagement.BackEnd.Models.RestaurantVisit;
using Hotelmanagement.BackEnd.Models.RestaurantVisitHotelGuest;
using Hotelmanagement.BackEnd.Models.Rooms;
using Hotelmanagement.BackEnd.Models.Service;
using Hotelmanagement.BackEnd.Models.TypesOfStay;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.TypesOfStay;
using Hotelmanagement.BackEnd.ViewModels.VisitService;
using Hotelmanagement.FrontEnd.Viewmodels;
using Hotelmanagement.FrontEnd.Viewmodels.Basedata;
using Hotelmanagement.FrontEnd.Viewmodels.Windows;
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
            UpdateVisitDatagrid();
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

        private void BookVisit(int visitId)
        {
            //Data objects for the visit
            Visit visit = VisitDB.GetVisitById(visitId); //Get visit by selected id
            MessageBox.Show(visit.Room_ID.ToString());
            VisitService visitService = VisitServiceDB.GetVisitServiceById(visitId); //Get visit service by selected visit id
            Rooms room = RoomsDB.GetRoomById(visit.Room_ID); //Get room by selected visit id
            RestaurantVisitHotelGuest restaurantVisitHotelGuest =
                RestaurantVisitHotelGuestDB.GetRestaurantVisitByVisitId(visitId); //Get restaurant visit by selected visit id

            double restaurantVisitPrice = 0;
            double servicePrice = 0;
            
            //Get Data for the service
            if (visitService != null)
            {
                int serviceId = visitService.ServiceOfferId; //Get service id from visit service
                Service service  = ServiceDB.GetServiceById(serviceId); //Get service by service id
                servicePrice = service.Price;
            }
            
            //Get data for the restaurant
            if (restaurantVisitHotelGuest != null)
            {
                int restaurantVisitId = restaurantVisitHotelGuest.RestaurantVisitID; //Get restaurant visit id from restaurant visit
                //Get restaurant visit by restaurant visit id
                RestaurantVisit restaurantVisit = RestaurantVisitDB.GetRestaurantVisitById(restaurantVisitId);
                restaurantVisitPrice = restaurantVisit.TotalCosts;
            }

            double subtotal = visit.Total_Costs; //Get visit subtotal
            
            double total = subtotal + servicePrice + restaurantVisitPrice; //Get total price

            VisitDB.UpdateVisit(new Visit(visitId, visit.Customer_ID, visit.Visit_Type_Of_Stay_ID, 
                visit.Room_ID, visit.Person_Amount, servicePrice, visit.Room_Costs, visit.Arrival, visit.Departure, 
                total, false, "", restaurantVisitPrice, false, false));
            
            //Open the discount checkout window to check if the customer wants to apply a discount
            DiscountCheckout discountCheckout = new DiscountCheckout(visitId);
            discountCheckout.Show();
            
        }
        
        
        //*****Tab Control*****
        
        //Main
        private void Switch_Main(object sender, RoutedEventArgs e)
        {
            ContentControl.Content = _transparent;
            ShowMain();
            Change_Tab(0);
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

        private void UpdateVisitDatagrid()
        {
            var dataset = VisitDB.GetDataSetVisits();
            ListView.ItemsSource = dataset.Tables[0].DefaultView;
        }

        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = ListView.SelectedItem;
            
            //Selected Item | id
            var id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            if(id == "")
            {
                MessageBox.Show("Keine ID");
                return;
            }

            VisitId.Text = id;

        }

        private void CreateInvoice(object sender, RoutedEventArgs e)
        {
            var id = VisitId.Text;
            if (id != "")
            {
                BookVisit(Int32.Parse(id));
            }
            else
            {
                MessageBox.Show("Es konnte kein Aufenthalt gefunden werden");
            }
        }
    }
}