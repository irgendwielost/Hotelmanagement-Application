using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.ViewModels.PaymentMethods;

public class PaymentMethodsDB
{
    public static DataSet GetDatasetPaymentMeth()
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
                
            var adapter = new MySqlDataAdapter("select * from `bezahlmethoden`", db.connection);
            DataSet dataSet = new();
            adapter.Fill(dataSet, "bezahlmethoden");
            return dataSet;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}