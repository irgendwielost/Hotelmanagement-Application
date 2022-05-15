using System;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.Models.AudienceFactors;
using Hotelmanagement.BackEnd.Models.Customer;
using Hotelmanagement.BackEnd.Models.Salary;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.FrontEnd.Viewmodels.Windows
{
    public partial class TargetAudienceFactors : Window
    {
        public TargetAudienceFactors(int id)
        {
            InitializeComponent();
            UpdateSalaryComboBox();
            CustomerId.Text = id.ToString();
            CustomerName.Text = GetCustomerName(id);
        }

        private string GetCustomerName(int id)
        {
            Customer customer = CustomerDB.GetCustomerById(id);
            return customer.Name;
        }
        private void UpdateSalaryComboBox()
        {
            var dataset = SalaryDB.GetSalary();
            Salary.ItemsSource = dataset.Tables["einkommen"]?.DefaultView;
        }
        private void AddTaf(object sender, RoutedEventArgs e)
        {
            var id = CustomerId.Text;
            var gender = Gender.Text;
            var travelReason = Travelreason.Text;
            var lifestyle = Lifestyle.Text;
            var familystate = Familystate.Text;
            var job = Job.Text;
            var company = Company.Text;
            var salary = "";
            if (Salary.SelectedValue != null)
            {
                salary = Salary.SelectedValue.ToString();
            }
            
            var businessTrip = businesstrip.IsChecked.Value;
            

           
                try
                {
                    AudienceFactorsDB.CreateCustomerTAF(new AudienceFactors(Int32.Parse(id), gender, 
                        travelReason, familystate, company, job, lifestyle,  
                        salary, businessTrip, false));
                    this.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Error: " + exception);
                }
         
            
        }
    }
}