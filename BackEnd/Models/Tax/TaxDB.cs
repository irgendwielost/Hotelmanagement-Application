using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Tax;

public class TaxDB
{
    public static global::Tax GetTaxById(int id)
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
            var cmd = new MySqlCommand($"SELECT * FROM `Steuer` WHERE ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new global::Tax(id, reader.GetString(1), reader.GetDouble(2));
            }
                
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return null;
        }

        return null;
    }
}