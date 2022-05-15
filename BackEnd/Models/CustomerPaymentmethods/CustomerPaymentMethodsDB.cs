using System;
using System.Data;
using System.Windows;
using Hotelmanagement.BackEnd.ViewModels.VisitService;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.CustomerPaymentmethods;

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
    
    public static void AddPaymentMeth(ViewModels.CustomerPaymentmethods.CustomerPaymentmethods paymentMeth)
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
            var cmd = new MySqlCommand($"INSERT INTO `kunde-bezahlmethoden` (Kunde_ID, Bezahlmethoden_ID, Bezahlinformation)" +
                                       $" VALUES (@customerId, @paymentMethId, @methodInfo)", db.connection);
            cmd.Parameters.AddWithValue("@customerId", paymentMeth.Customer_ID);
            cmd.Parameters.AddWithValue("@paymentMethId", paymentMeth.Paymentmethod_ID);
            cmd.Parameters.AddWithValue("@methodInfo", paymentMeth.Method_Information);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Dem Kunden konnten keine Bezahlmethode hinzugefügt werden\n" 
                            + "Error:" + ex.Message);
        }
    }
    
}