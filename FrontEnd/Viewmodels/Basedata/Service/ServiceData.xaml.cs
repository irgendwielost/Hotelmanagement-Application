using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Hotelmanagement.BackEnd.Models.Service;

namespace Hotelmanagement.FrontEnd.Viewmodels.Basedata.Service
{
    public partial class ServiceData : UserControl
    {
        public ServiceData()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        public void UpdateDataGrid()
        {
            var dataset = ServiceDB.GetDataSetService();
            ListView.ItemsSource = dataset.Tables["serviceangebote"]?.DefaultView;
        }
        private void OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Kategorie_ID")
            {
                e.Column.Header = "Kategorie";
            }
            if (propertyDescriptor.DisplayName == "Dauer-Minuten")
            {
                e.Column.Header = "Dauer";
            }
            if (propertyDescriptor.DisplayName == "Beschreibung")
            {
                e.Cancel = true;
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

        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = ListView.SelectedItem; 
            
            //Selected Item | id
            var id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | name
            var name = (ListView.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | size
            var price = (ListView.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | amount
            var category_id /* change to category name? */ = (ListView.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | extra bed capacity
            var duration = (ListView.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;


            if (id != null)
            {
                MessageBox.Show(id.ToString());
                GetDescription(Int32.Parse(id));
            }
        }
        
        private void GetDescription(int id)
        {
            BackEnd.Models.Service.Service service = ServiceDB.GetServiceById(id);
            DescriptionBox.Text = service.Description;
        }
    }
}