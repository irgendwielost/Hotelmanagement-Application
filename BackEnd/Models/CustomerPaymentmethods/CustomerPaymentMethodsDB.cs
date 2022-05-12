using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.ViewModels.CustomerPaymentmethods;

public class CustomerPaymentMethodsDB
{
    public static DataSet GetDataSetCustomerPayMeth(int customerId)
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

            var adapter = new MySqlDataAdapter("select `kunde-bezahlmethoden`.* , " +
                                               "b.Name as PayMethName from `kunde-bezahlmethoden` " +
                                               "join bezahlmethoden b on b.ID = `kunde-bezahlmethoden`.Bezahlmethoden_ID " +
                                               $"WHERE Kunde_ID={customerId}", db.connection);
            DataSet dataSet = new();
            adapter.Fill(dataSet, "kunde-bezahlmethoden");
            return dataSet;
        }
        catch
        {
            return null;
        }
    }
}