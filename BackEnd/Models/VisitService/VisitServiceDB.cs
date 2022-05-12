using System;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.VisitService;
using MySql.Data.MySqlClient;

public class VisitServiceDB
{
    public static VisitService GetVisitServiceById(int id)
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
            var cmd = new MySqlCommand($"SELECT * FROM `besuch-service` WHERE Besuch_ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new VisitService(id, reader.GetInt32(1), reader.GetDateTime(2));
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