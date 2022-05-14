using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Employee;

public class EmployeeDB
{
    public static ViewModels.Customer.Employee GetEmployeeByName(string username, string password)
    {
        using var db = new Database.Database();
            
        try
        {
            db.connection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine($"table opening error{e}");
        }
            
        try
        {
            var cmd = new MySqlCommand($"SELECT * FROM `mitarbeiter` WHERE Name='{username}' AND Passwort = '{password}'",
                db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new ViewModels.Customer.Employee(reader.GetInt32(0), reader.GetString(1), 
                    reader.GetInt32(2), reader.GetBoolean(3));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Es konnte kein Mitarbeiter gefunden werden\n" + ex.Message);
            return null;
        }

        return null;
    }
}