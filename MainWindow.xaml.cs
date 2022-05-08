using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Hotelmanagement.BackEnd.Models.Rooms;
using Hotelmanagement.BackEnd.Models.TypesOfStay;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.TypesOfStay;
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
            
            if(id == null)
            {
                MessageBox.Show("Keine ID");
                return;
            }

            //Selected Item | name
            var customerId = (ListView.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;


            //Selected Item | birthday
            var roomId = (ListView.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | email
            var typeOfStayId = (ListView.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
            var email = (ListView.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;
            

            //Selected Item | street
            var street = (ListView.SelectedCells[5].Column.GetCellContent(item) as TextBlock)?.Text;
            
            var place = (ListView.SelectedCells[6].Column.GetCellContent(item) as TextBlock)?.Text;
            
            var postalcode = (ListView.SelectedCells[7].Column.GetCellContent(item) as TextBlock)?.Text;
        }

        private void CreateInvoice(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Neue Rechnung erstellt");
            
        }
    }
}