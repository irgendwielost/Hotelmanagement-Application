using System;
using System.Data;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.Models.CustomerDiscounts;
using MySql.Data.MySqlClient;
public class CustomerDiscountsDB
{
    public static DataSet GetDataSetCustomerDiscountsById(int id)
    {
        using var db = new Database();
            
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
                
            var adapter = new MySqlDataAdapter($"SELECT * FROM `kunden-rabatte` WHERE Kunde_ID={id}", db.connection);
            DataSet dataSet = new();
            adapter.Fill(dataSet, "kunden-rabatte");
            return dataSet;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
        return null;
    }

    public static CustomerDiscounts GetCustomerDiscountsById(int id)
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
            var cmd = new MySqlCommand($"SELECT * FROM `kunden-rabatte` WHERE ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
            {
                MessageBox.Show("Der Reader redet keine kunden rabatte bro");
            }
            else
            {
                return new CustomerDiscounts(id, reader.GetInt32(1), reader.GetString(2),
                    reader.GetDouble(3));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Der Kundenrabatt konnte nicht abgerufen werden\n" + ex.Message);
            return null;
        }

        return null!;
    }
}