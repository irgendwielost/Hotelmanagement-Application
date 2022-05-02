using System;
using System.ComponentModel;
using System.Data;
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
                e.Column.Header = "Nice";
            }
            if (propertyDescriptor.DisplayName == "Entfernt")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "EntferntAm")
            {
                e.Cancel = true;
            }
        }

        private void OpenTargetAudienceFactorsWindow(object sender, RoutedEventArgs e)
        {
            TargetAudienceFactors targetAudienceFactors = new TargetAudienceFactors(Int32.Parse(1.ToString()));
            targetAudienceFactors.Show();
        }
    }
}