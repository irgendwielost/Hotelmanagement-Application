using System.ComponentModel;
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
            ListView.ItemsSource = dataset.Tables["zimmer"]?.DefaultView;
        }
        private void OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor)e.PropertyDescriptor;
            e.Column.Header = propertyDescriptor.DisplayName;
            if (propertyDescriptor.DisplayName == "Groesse")
            {
                e.Column.Header = "Größe";
            }

            if (propertyDescriptor.DisplayName == "Standartpreis")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "Beschreibung")
            {
                e.Cancel = true;
            }
            if (propertyDescriptor.DisplayName == "ID")
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
    }
}