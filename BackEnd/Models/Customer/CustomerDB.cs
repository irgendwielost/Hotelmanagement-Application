using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Customer;

public class CustomerDB
{
    public static DataSet GetDataSetCustomer()
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
                
            var adapter = new MySqlDataAdapter("select * from `kunden`", db.connection);
            DataSet dataSet = new();
            adapter.Fill(dataSet, "kunden");
            return dataSet;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return null;
    }
}