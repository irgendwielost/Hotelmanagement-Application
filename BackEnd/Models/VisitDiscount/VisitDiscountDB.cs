using System;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.Models.Visit;
using Hotelmanagement.BackEnd.ViewModels.VisitDiscount;
using MySql.Data.MySqlClient;



public class VisitDiscountDB
{
    public static void AddVisitDiscount(VisitDiscount visitDiscount)
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
            var cmd = new MySqlCommand($"INSERT INTO `besuch-rabatt` (Besuch_ID, Kundenrabatt_ID, " +
                                       $"Angebotsaktion_ID, Wert) VALUES ({visitDiscount.Visit_ID}, " +
                                       $"{visitDiscount.Customer_Discount_ID}, {visitDiscount.Special_Offer_ID}, " +
                                       $"{visitDiscount.Value.ToString().Replace(",",".")})",
                db.connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Konnte Rabatt nicht eintragen: \n" + ex.Message);
        }
    }
}