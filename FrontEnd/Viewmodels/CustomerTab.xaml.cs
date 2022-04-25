using System.Data;
using System.Windows.Controls;
using Hotelmanagement.BackEnd.ViewModels.Department;

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
            var dataset = DepartmentDB.GetDataSetDepartment();
            ListView.ItemsSource = dataset.Tables["abteilung"]?.DefaultView;
        }
    }
}