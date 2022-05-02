using System.Windows;
using System.Windows.Controls;
using Hotelmanagement.BackEnd.Models.Visit;

namespace Hotelmanagement.FrontEnd.Viewmodels
{
    public partial class RestaurantTab : UserControl
    {
        public RestaurantTab()
        {
            InitializeComponent();
        }
        
        private void CalculateAvailableRooms(int id)
        {
            VisitDB.GetRoomsInVisitsById(id);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            CalculateAvailableRooms(1);
        }
    }
}