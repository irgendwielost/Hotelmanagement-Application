using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotelmanagement.BackEnd.Models.Customer;
using Hotelmanagement.BackEnd.Models.Rooms;
using Hotelmanagement.BackEnd.Models.TypesOfStay;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.TypesOfStay;
using Hotelmanagement.FrontEnd.Viewmodels.Basedata.Hotel;

namespace Hotelmanagement.FrontEnd.Viewmodels
{
    public partial class BookingTab : UserControl
    {
        public BookingTab()
        {
            InitializeComponent();
            UpdateRoomComboBox();
            UpdateCustomerComboBox();
            UpdateTypeOfStayComboBox();
        }

        
        public void UpdateCustomerComboBox()
        {
            try
            {
                var dataset = CustomerDB.GetDataSetCustomer();
                 CustomerComboBox.ItemsSource = dataset.Tables[0].DefaultView;   
            }
            catch (Exception e)
            {
                MessageBox.Show("Daten zu den Kunden konnten nicht geladen werden.\n" + "Error:" + e.Message);
                Console.WriteLine(e);
            }
        }
        public void UpdateRoomComboBox()
        {
            try
            {
                var dataset = RoomsDB.GetDataSetRooms();
                RoomComboBox.ItemsSource = dataset.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show("Daten zu den Zimmern konnten nicht geladen werden.\n" +"Error:"+  e.Message);
            }
        }

        public void UpdateTypeOfStayComboBox()
        {
            try
            {
                var dataset = TypesOfStayDB.GetDataSetTypesOfStay();
                TypeOfStayComboBox.ItemsSource = dataset.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show("Daten zu den Aufenthaltsarten konnten nicht geladen werden.\n" +"Error:"+  e.Message);
            }
        }
        
        private void SwitchToCustomer(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Neuer Kunde wird erstellt...");
        }

        //Make sure that the input is a number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private int CalculateAvailableRooms(int id)
        {
            return VisitDB.GetRoomsInVisitsById(id);
            
        }
        
        private void RoomComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = RoomComboBox.SelectedValue;
            Rooms room = RoomsDB.GetRoomById(Int32.Parse(id.ToString() ?? string.Empty));
            
            //Get the number of used rooms by room id
            int roomCount = VisitDB.GetRoomsInVisitsById(Int32.Parse(id.ToString() ?? string.Empty));
            
            int availableRooms = room.Amount - roomCount;

            if (availableRooms == 0)
            {
                MessageBox.Show("Jedes dieser Zimmer ist belegt");
            }
            
            RoomName.Text = room.Designation;   //Room name
            SizeTextBox.Text = room.Size; //Room size
            ExtraBedCapacityTextBox.Text = room.ExtraBedCapacity.ToString();    //Extra bed capacity
            ExtraBedPriceTextBox.Text = room.ExtraBedPrice.ToString(CultureInfo.InvariantCulture);  //Extra bed price
            RoomPriceTextBox.Text = room.BasePrice.ToString(CultureInfo.InvariantCulture);  //Room price
            SituationTextBox.Text = room.Situation;  //Room situation
            DescriptionTextBox.Text = room.Description; //Room description
            AvailableTextBox.Text = availableRooms.ToString(); //Available rooms
        }

        private void CalculatePrice(object sender, RoutedEventArgs e)
        {
            try
            {
                //Get the values from the input fields
                var roomId = RoomComboBox.SelectedValue;
                var typeOfStayId = TypeOfStayComboBox.SelectedValue;
                
                //Get a room object by id 
                Rooms room = RoomsDB.GetRoomById(Int32.Parse(roomId.ToString() ?? string.Empty));
                double price = room.BasePrice;
                
                //Get a type of stay object by id
                TypesOfStay typesOfStay = TypesOfStayDB.GetTypeOfStayByID(Int32.Parse(typeOfStayId.ToString() ?? string.Empty));
                double stayPrice = typesOfStay.Price_change;
                
                //Check if dates are picked and get the values
                if (BeginDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null)
                {
                    DateTime startDate = BeginDatePicker.SelectedDate.Value;
                    DateTime endDate = EndDatePicker.SelectedDate.Value;
                    var numberOfDays = (endDate - startDate).TotalDays;
                    
                    double roomPrice = price * numberOfDays;
                    double totalPrice = roomPrice + stayPrice;
                    
                    if(ExtraBedCheckBox.IsChecked == true)
                    {
                        double extraBedPrice = Int32.Parse(ExtraBedTextBox.Text) * room.ExtraBedPrice;
                        totalPrice += extraBedPrice;
                    }
                    
                    TotalPriceTextBlock.Text = totalPrice.ToString(CultureInfo.InvariantCulture);
                   
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie ein Start- und Enddatum ein.");
                }

                
                

            }
            catch (Exception exception)
            {
                MessageBox.Show("Es sind nicht alle Daten richtig eingegeben.\n" + exception.Message);
                Console.WriteLine(exception);
            }
            
            
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CalculateAvailableRooms(1);
        }
    }
}