using System.Windows;
using Hotelmanagement.BackEnd.Models.Employee;
using Hotelmanagement.BackEnd.ViewModels.Customer;

namespace Hotelmanagement.FrontEnd.Windows;

public partial class LogIn : Window
{
    public LogIn()
    {
        InitializeComponent();
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        Close();
    }
    
    private void UserLogIn(object sender, RoutedEventArgs e)
    {
        var name = NameTextBox.Text;
        var password = PasswordBox.Password;

        Employee employee = EmployeeDB.GetEmployeeByName(name, password);
        if (employee != null)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        else
        {
            MessageBox.Show("Benutzername oder Passwort falsch!");
        }
    }
}