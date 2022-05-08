using System;
using System.Windows;
using Hotelmanagement.BackEnd.ViewModels.VisitService;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.RestaurantVisitHotelGuest;

public class RestaurantVisitHotelGuestDB
{
    public static RestaurantVisitHotelGuest GetRestaurantVisitByVisitId(int id)
    {
        using var db = new Database.Database();
            
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
                return new RestaurantVisitHotelGuest(reader.GetInt32(0), reader.GetInt32(1), id, 
                    reader.GetDateTime(3));
            }
                
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            MessageBox.Show("Es ist ein Fehler aufgetreten");
            return null;
        }

        return null;
    }
}