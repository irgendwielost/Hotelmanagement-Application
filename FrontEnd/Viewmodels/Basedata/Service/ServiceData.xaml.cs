using System;
using System.ComponentModel;
using System.Data;
using System.Security.Principal;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Hotelmanagement.BackEnd.Models.Department;
using Hotelmanagement.BackEnd.Models.Service;
using Hotelmanagement.BackEnd.ViewModels.Department;

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
        }
        
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             
            //Selected Item
            object item = ListView.SelectedItem;
            if (item != null)
            {
                //Selected Item | id
                var id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
                
                if(id == "")
                {
                    MessageBox.Show("Keine ID");
                    return;
                }

                if (id != "")
                {
                    GetDescription(Int32.Parse(id));
                }
            }
                
        }

        private void AddService(object sender, RoutedEventArgs e)
        {
            //Selected Item
            object item = ListView.SelectedItem;
            var id = "0";
            
            if (item != null)
            {
                //Selected Item | id
                id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;

                if (id == "")
                {
                    id = "0";
                }
                //Selected Item | name
                var name = (ListView.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;

                //Selected Item | size
                var price = (ListView.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;

                //Selected Item | amount
                var category_id /* change to category name? */ =
                    (ListView.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;

                //Selected Item | extra bed capacity
                var duration = (ListView.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;

                string description = DescriptionBox.Text;
            
                ServiceDB.AddService(new BackEnd.Models.Service.Service(Int32.Parse(id), name, 
                    description, Double.Parse(price), Int32.Parse(category_id), 
                    Int32.Parse(duration), false));
            }
            
        }
        private void GetDescription(int id)
        {
            BackEnd.Models.Service.Service service = ServiceDB.GetServiceById(id);
            DescriptionBox.Text = service.Description;
        }


        private void DeleteService(object sender, RoutedEventArgs e)
        {
            //Selected Item
            object item = ListView.SelectedItem;
            
            //Selected Item | id
            var id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
            if (item != null)
            {
                ServiceDB.DeleteService(Int32.Parse(id));
            }
            else
            {
                MessageBox.Show("Kein Service ausgewählt");
            }
            
            
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}