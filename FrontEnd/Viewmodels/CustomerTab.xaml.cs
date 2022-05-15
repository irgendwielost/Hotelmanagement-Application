using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hotelmanagement.BackEnd.Models.Customer;
using Hotelmanagement.BackEnd.Models.CustomerPaymentmethods;
using Hotelmanagement.BackEnd.Models.Department;
using Hotelmanagement.BackEnd.ViewModels.CustomerPaymentmethods;
using Hotelmanagement.BackEnd.ViewModels.Department;
using Hotelmanagement.BackEnd.ViewModels.PaymentMethods;
using Hotelmanagement.FrontEnd.Viewmodels.Windows;

namespace Hotelmanagement.FrontEnd.Viewmodels
{
    public partial class CustomerTab : UserControl
    {
        public CustomerTab()
        {
            InitializeComponent();
            UpdateDataGrid();
            UpdatePaymentMethCombobox();
        }

        private void UpdateDataGrid()
        {
            var dataset = CustomerDB.GetDataSetCustomer();
            ListView.ItemsSource = dataset.Tables["kunden"]?.DefaultView;
        }
        
        public void UpdatePaymentMethCombobox()
        {
            try
            {
                var dataset = PaymentMethodsDB.GetDatasetPaymentMeth();
                PaymentMethodCombobox.ItemsSource = dataset.Tables[0].DefaultView;
            }
            catch (Exception e)
            {
                MessageBox.Show("Daten zu den Bezahlmethoden konnten nicht geladen werden.\n"
                                +"Error:"+  e.Message);
            }
        }
        
        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Entfernt")
            {
                e.Cancel = true;
            }

        }
        private void AddCustomer(object sender, RoutedEventArgs e)
        {
            var id = "0";
            var customerId = hiddenId.Text;
            var methInfo = methodInfoText.Text;
            var name = Name.Text;
            var email = Email.Text;
            var birthday = this.Birthday.SelectedDate;
            var telephone = Tel.Text;
            var address = Street.Text;
            var place = Place.Text;
            var zip = PostalCode.Text;
            var paymentMeth = PaymentMethodCombobox.SelectedValue;
            
            if(name != "" && email != "" && telephone != "" && address != "" && place != "" && zip != "" )
            {
                if (birthday != null)
                {
                    var selectedBirthday = birthday.Value.ToShortDateString();
                    if (customerId == "")
                    {
                        customerId = "0";
                    }   
                    long customerID = CustomerDB.CreateCustomer(new Customer(Int32.Parse(customerId), name, 
                        DateTime.Parse(selectedBirthday), telephone, email,
                        address, place, zip, false));
                    
                    if (methInfo != "" && paymentMeth != null)
                    {
                        CustomerPaymentMethodsDB.AddPaymentMeth(new CustomerPaymentmethods(0, Int32.Parse(customerID.ToString()), 
                            Int32.Parse(paymentMeth.ToString() ?? string.Empty), methInfo, false));
                    }
                    else
                    {
                        MessageBox.Show("Bitte geben Sie eine Bezahlmethode und eine Bezahlmethode-Info an.");
                    }
                    
                    hiddenId.Text = "0";
                    Name.Text = "";
                    Email.Text = "";
                    Tel.Text = "";
                    Street.Text = "";
                    Place.Text = "";
                    PostalCode.Text = "";
                    Birthday.SelectedDate = null;
                    PaymentMethodCombobox.SelectedValue = 0;
                    methodInfoText.Text = "";
                    
                    if (customerID != 0)
                    {
                        if(MessageBox.Show("Möchten sie noch Zielgruppenfaktoren hinzufügen?", "Question", 
                            MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            TargetAudienceFactors targetAudienceFactors = new TargetAudienceFactors(Int32.Parse(customerID.ToString()));
                            targetAudienceFactors.Show();
                            UpdateDataGrid();
                        }
                        else
                        {
                            UpdateDataGrid();
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
            if (item != null)
            {
                var id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;

                if (id == "")
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
            var id = "0";
            if (item != null)
            {
                //Selected Item | id
                id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
                
                hiddenId.Text = id;
                if(id == null)
                {
                    MessageBox.Show("Keine ID");
                    return;
                }
            }
            else
            {
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
            Birthday.SelectedDate = Convert.ToDateTime(birthday);
            Tel.Text = phone;
            Email.Text = email;
            Street.Text = street;
            Place.Text = place;
            PostalCode.Text = postalcode;
            
        }
        
        //Make sure that the input is a number
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            hiddenId.Text = "0";
            Name.Text = "";
            Email.Text = "";
            Tel.Text = "";
            Street.Text = "";
            Place.Text = "";
            PostalCode.Text = "";
            Birthday.SelectedDate = null;
            PaymentMethodCombobox.SelectedValue = 0;
            methodInfoText.Text = "";
        }
    }
}