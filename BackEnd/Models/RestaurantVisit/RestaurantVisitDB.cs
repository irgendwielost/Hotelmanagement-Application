using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.RestaurantVisit;

public class RestaurantVisitDB
{
    public static RestaurantVisit GetRestaurantVisitById(int id)
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
            var cmd = new MySqlCommand($"SELECT * FROM `restaurant-besuch` WHERE ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new RestaurantVisit(id, reader.GetInt32(1), reader.GetDouble(2), 
                    reader.GetBoolean(3));
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