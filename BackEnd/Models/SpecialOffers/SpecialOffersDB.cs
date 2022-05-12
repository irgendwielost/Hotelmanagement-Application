using System;
using System.Data;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.ViewModels.SpecialOffers;
using MySql.Data.MySqlClient;

public class SpecialOffersDB
{
    public static DataSet GetDataSetSpecialOffers()
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
                
            var adapter = new MySqlDataAdapter("select * from `angebotsaktionen`", db.connection);
            DataSet dataSet = new();
            adapter.Fill(dataSet, "angebotsaktionen");
            return dataSet;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return null;
    }
    
    public static SpecialOffers GetSpecialOffersById(int id)
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
            var cmd = new MySqlCommand($"SELECT * FROM `angebotsaktionen` WHERE ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new SpecialOffers(id, reader.GetString(1), reader.GetDateTime(2), 
                    reader.GetDateTime(3),reader.GetDouble(4), reader.GetBoolean(5));
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