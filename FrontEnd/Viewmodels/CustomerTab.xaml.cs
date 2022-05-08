using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Hotelmanagement.BackEnd.Models.Customer;
using Hotelmanagement.BackEnd.Models.Department;
using Hotelmanagement.BackEnd.ViewModels.Department;
using Hotelmanagement.FrontEnd.Viewmodels.Windows;

namespace Hotelmanagement.FrontEnd.Viewmodels
{
    public partial class CustomerTab : UserControl
    {
        public CustomerTab()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            var dataset = CustomerDB.GetDataSetCustomer();
            ListView.ItemsSource = dataset.Tables["kunden"]?.DefaultView;
        }
        
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "ID")
            {
                e.Column.Header = "ID";
            }
            if (propertyDescriptor.DisplayName == "Entfernt")
            {
                e.Cancel = true;
            }
        }

        private void OpenTargetAudienceFactorsWindow(object sender, RoutedEventArgs e)
        {
            TargetAudienceFactors targetAudienceFactors = new TargetAudienceFactors(Int32.Parse(1.ToString()));
            targetAudienceFactors.Show();
        }

        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            var id = "0";
            var name = Name.Text;
            var email = Email.Text;
            var birthday = this.Birthday.SelectedDate;
            var telephone = Tel.Text;
            var address = Street.Text;
            var place = Place.Text;
            var zip = PostalCode.Text;
            
            if(name != "" && email != "" && telephone != "" && address != "" && place != "" && zip != "")
            {
                if (birthday != null)
                {
                    var selectedBirthday = birthday.Value.ToShortDateString();
                    
                    long customerID = CustomerDB.CreateCustomer(new Customer(Int32.Parse(id), name, 
                        DateTime.Parse(selectedBirthday), telephone, email,
                        address, place, zip, false));
                    Name.Text = "";
                    Email.Text = "";
                    Tel.Text = "";
                    Street.Text = "";
                    Place.Text = "";
                    PostalCode.Text = "";
                    Birthday.SelectedDate = null;
                    UpdateDataGrid();
                
                    if (customerID != 0)
                    {
                        if(MessageBox.Show("Möchten sie noch Zielgruppenfaktoren hinzufügen?", "Question", 
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            TargetAudienceFactors targetAudienceFactors = new TargetAudienceFactors(Int32.Parse(customerID.ToString()));
                            targetAudienceFactors.Show();
                        }
                    }
                }
                
               
                
            }
            else
            {
                MessageBox.Show("Bitte füllen Sie alle Felder aus!");
            }
        }

        private void DeleteCustomer(object sender, RoutedEventArgs e)
        {
            //Selected Item
            object item = ListView.SelectedItem; 
            
            //Selected Item | id
            if (item != null && item != "")
            {
                var id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;

                if (string.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Es wurde keine ID gefunden");
                }
                else
                {
                    CustomerDB.DeleteCustomer(Int32.Parse(id));
                    UpdateDataGrid();
                }
            }
            else
            {
                MessageBox.Show("Es wurde kein Kunde ausgewählt");
            }
            
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
            var name = (ListView.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;


            //Selected Item | birthday
            var birthday = (ListView.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;

            //Selected Item | email
            var phone = (ListView.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
            var email = (ListView.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;
            

            //Selected Item | street
            var street = (ListView.SelectedCells[5].Column.GetCellContent(item) as TextBlock)?.Text;
            
            var place = (ListView.SelectedCells[6].Column.GetCellContent(item) as TextBlock)?.Text;
            
            var postalcode = (ListView.SelectedCells[7].Column.GetCellContent(item) as TextBlock)?.Text;

            Name.Text = name;
            //Birthday.Text = birthday;
            Tel.Text = phone;
            Email.Text = email;
            Street.Text = street;
            Place.Text = place;
            PostalCode.Text = postalcode;
        }
    }
}