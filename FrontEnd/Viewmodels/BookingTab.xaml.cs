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
using Hotelmanagement.BackEnd.ViewModels.VisitRoom;
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

        private void RoomComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = RoomComboBox.SelectedValue;
            if (id != null)
            {
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
            
        }

        private void CalculatePrice()
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
            }
            catch (Exception exception)
            {
                MessageBox.Show("Es sind nicht alle Daten richtig eingegeben.\n" + exception.Message);
            }
        }
        private void BookRoom(object sender, RoutedEventArgs e)
        {
            CalculatePrice();
            //Set ids to 0 to avoid null reference exception
            int roomId = 0;
            int customerId = 0;
            int typeOfStay = 0;
            
            //Check if a room is selected
            if(RoomComboBox.SelectedValue != null)
            {
                roomId = (int)RoomComboBox.SelectedValue;
            }
            else
            {
                MessageBox.Show("Bitte ein Zimmer auswählen");
            }
            
            //Check if a customer is selected
            if (CustomerComboBox.SelectedValue != null)
            {
                customerId = (int)CustomerComboBox.SelectedValue;
            }
            else
            {
                MessageBox.Show("Bitte einen Kunden auswählen");
            }
            
            //Check if a type of stay is selected
            if (TypeOfStayComboBox.SelectedValue != null)
            {
                typeOfStay = (int) TypeOfStayComboBox.SelectedValue;
            }
            else
            {
                MessageBox.Show("Bitte eine Aufenthaltsart auswählen");
            }
            
            DateTime arrival = new DateTime(0);
            DateTime departure = new DateTime(0);
            
            // Check if dates are picked and get the values
            if (BeginDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null)
            {
                arrival = BeginDatePicker.SelectedDate!.Value;
                departure = EndDatePicker.SelectedDate!.Value;
            }
            
            var personAmount = PersonAmount.Text;
            var extraBedAmount = 0;
            double roomPrice = 0;
            bool extraBed = false;
            
            
            //Calculation for the room costs
            if (roomId != 0)
            {
                Rooms room = RoomsDB.GetRoomById(roomId);
                double roomBasePrice = room.BasePrice;    
                
                var numberOfDays = (departure - arrival).TotalDays;
                    
                roomPrice = roomBasePrice * numberOfDays;
            }
            
            
            if (ExtraBedCheckBox.IsChecked == true && ExtraBedTextBox.Text != "")
            {
                extraBedAmount = Int32.Parse(ExtraBedTextBox.Text);
                extraBed = true;
            }

            var selectedArrival = arrival.ToShortDateString();
            var selectedDeparture = departure.ToShortDateString();
            
            if (TotalPriceTextBlock.Text != "0" && personAmount != "" && customerId != 0 && roomId != 0 
                && typeOfStay != 0 && BeginDatePicker.SelectedDate != null && EndDatePicker.SelectedDate != null)
            {
                var totalPrice = TotalPriceTextBlock.Text;
                var visitId = VisitDB.AddVisit(new Visit(0, customerId, typeOfStay, 
                    roomId, Int32.Parse(personAmount), 0, roomPrice, 
                    DateTime.Parse(selectedArrival), DateTime.Parse(selectedDeparture), Double.Parse(totalPrice),
                    false, "", 0, false, false,false, 0));
                VisitRoomDB.AddVisitroom(new VisitRoom((int)visitId, roomId, extraBed, extraBedAmount));
                MessageBox.Show("Zimmer wurde gebucht");
                ClearFields();
            }
            else
            {
                MessageBox.Show("Überprüfen sie Ihre Eingaben");
            }
        }

        private void ClearFields()
        {
            //clear all input fields
            RoomComboBox.SelectedValue = 0;
            CustomerComboBox.SelectedValue = 0;
            BeginDatePicker.SelectedDate = null;
            BeginDatePicker.DisplayDate = DateTime.Today;
            EndDatePicker.SelectedDate = null;
            EndDatePicker.DisplayDate = DateTime.Today;
            TypeOfStayComboBox.SelectedValue = 0;
            ExtraBedTextBox.Text = "";
            TotalPriceTextBlock.Text = "0";
            PersonAmount.Text = "";
            SizeTextBox.Text = "";
            AvailableTextBox.Text = "";
            DescriptionTextBox.Text = "";
            SituationTextBox.Text = "";
            ExtraBedCapacityTextBox.Text = "";
            ExtraBedPriceTextBox.Text = "";
            RoomPriceTextBox.Text = "";
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            UpdateCustomerComboBox();
            UpdateRoomComboBox();
            UpdateTypeOfStayComboBox();
        }
    }
}