using System;
using System.Windows;
using Hotelmanagement.BackEnd.Models.Visit;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.ViewModels.VisitDiscount;

public class VisitDiscountDB
{
    public static void AddVisitDiscount(VisitDiscount visitDiscount)
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