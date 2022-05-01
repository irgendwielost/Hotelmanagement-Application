using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotelmanagement.BackEnd.Models.Customer;
using Hotelmanagement.BackEnd.Models.Rooms;
using Hotelmanagement.BackEnd.ViewModels.TypesOfStay;

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
            Rooms room = RoomsDB.GetRoomById(Int32.Parse(id.ToString() ?? string.Empty));
            //Room name
            RoomName.Text = room.Designation;
            
        }
    }
}