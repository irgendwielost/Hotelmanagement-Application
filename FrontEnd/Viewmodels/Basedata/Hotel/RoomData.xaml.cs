using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Hotelmanagement.BackEnd.Models.Rooms;

namespace Hotelmanagement.FrontEnd.Viewmodels.Basedata.Hotel
{
   
    public partial class RoomData : UserControl
    {
        public RoomData()
        {
            InitializeComponent();
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            var dataset = RoomsDB.GetDataSetRooms();
            ListView.ItemsSource = dataset.Tables["zimmer"].DefaultView;
        }
        private void OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Groesse")
            {
                e.Column.Header = "Größe";
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

        //On Selected Datagrid Row
        private void DataGrid_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Selected Item
            object item = ListView.SelectedItem;

            if (item != null)
            {
                //Selected Item | id
                var id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;
            
                //Check if id is null
                if(id == null)
                {
                    return;
                }
            
                //Display description of room
                if (id != "")
                {
                    GetDescription(Int32.Parse(id));
                }
            }
            
        }
        
        private void AddRoom(object sender, RoutedEventArgs e)
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
            }
            
            
            //Selected Item | name
            var name = (ListView.SelectedCells[1].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | size
            var size = (ListView.SelectedCells[2].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | amount
            var price = (ListView.SelectedCells[3].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | amount
            var amount = (ListView.SelectedCells[4].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | extra bed capacity
            var extraBedCapacity = (ListView.SelectedCells[5].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | extra bed price
            var extraBedPrice = (ListView.SelectedCells[6].Column.GetCellContent(item) as TextBlock)?.Text;
            
            //Selected Item | room sitatuion
            var situation = (ListView.SelectedCells[7].Column.GetCellContent(item) as TextBlock)?.Text;

            var description = DescriptionBox.Text;
            
            RoomsDB.CreateRoom(new Rooms(Int32.Parse(id), name, size, Double.Parse(price), 
                Int32.Parse(amount), description, Int32.Parse(extraBedCapacity), 
            Double.Parse(extraBedPrice), situation, false));
            UpdateDataGrid();
        }
        private void GetDescription(int id)
        {
            Rooms room = RoomsDB.GetRoomById(id);
            DescriptionBox.Text = room.Description;
        }

        private void Refresh(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void DeleteRoom(object sender, RoutedEventArgs e)
        {
            //Selected Item
            object item = ListView.SelectedItem;
            var id = "0";
            
            if (item != null)
            {
                //Selected Item | id
                id = (ListView.SelectedCells[0].Column.GetCellContent(item) as TextBlock)?.Text;

                if (id != null) RoomsDB.DeleteRoom(Int32.Parse(id));
                else
                {
                    MessageBox.Show("Es ist kein Raum ausgewählt");
                }
            }
            
        }
        
    }
}