using System;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.Models.RestaurantVisitHotelGuest;
using Hotelmanagement.BackEnd.ViewModels.VisitService;
using MySql.Data.MySqlClient;
public class RestaurantVisitHotelGuestDB
{
    public static RestaurantVisitHotelGuest GetRestaurantVisitByVisitId(int id)
    {
        using var db = new Database();
            
        try
        {
            db.connection.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine($"table opening error{e}");
            throw;
        }
            
        try
        {
            var cmd = new MySqlCommand($"SELECT * FROM `restaurant-besuch-hotelgast` WHERE Besuch_ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new RestaurantVisitHotelGuest(reader.GetInt32(0), reader.GetInt32(1), 
                    reader.GetDateTime(3), id);
            }
                
        }
        catch (Exception ex)
        {
            MessageBox.Show("Die Daten zu dem Restaurant Aufenthalt konnten nicht abgerufen werden\n" + ex.Message);
            return null;
        }

        return null;
    }
}