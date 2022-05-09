using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Invoice;

public class InvoiceDB
{
    public static DataSet GetDataSetInvoice()
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
                
            var adapter = new MySqlDataAdapter("select * from `rechnung`", db.connection);
            DataSet dataSet = new();
            adapter.Fill(dataSet, "rechnung");
            return dataSet;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return null;
    }
    
    public static long CreateInvoice(Invoice invoice)
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
            var cmd = new MySqlCommand($"INSERT INTO `rechnung` (Besuch_ID, Besuch_Gesamtkosten, " +
                                       $"Zimmer_Kosten, ServiceKosten, RestaurantKosten, Kunden_Rabatt_Wert, " +
                                       $"Angebotsaktion_Wert, Steuer_ID, Steuersumme, Bezahlt, Entfernt) " +
                                       $"VALUES ({invoice.VisitID}, " +
                                       $"{invoice.VisitTotalCosts.ToString().Replace(",",".")}, " +
                                       $"{invoice.Room_Costs.ToString().Replace(",",".")}," +
                                       $"{invoice.Service_Costs.ToString().Replace(",",".")}, " +
                                       $"{invoice.Restaurant_Costs.ToString().Replace(",",".")}, " +
                                       $"{invoice.Customer_Discount_Value}, {invoice.Specialoffers_Value}, {invoice.Tax_Id}," +
                                       $"{invoice.Tax_Sum.ToString().Replace(",",".")}, " +
                                       $"{invoice.Paid}, {invoice.Deleted})", db.connection);
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Es konnte keine Rechnung erstellt werden\n" + "Error:" + ex.Message);
        }

        return 0;
    } 
    
    public static long UpdateInvoice(Invoice invoice)
    {
        using var db = new Database.Database();

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
            var cmd = new MySqlCommand($"UPDATE `rechnung` SET " +
                                       $"Besuch_Gesamtkosten = {invoice.VisitTotalCosts}," +
                                       $"Zimmer_Kosten = {invoice.Room_Costs.ToString().Replace(",",".")}," +
                                       $"ServiceKosten = {invoice.Service_Costs.ToString().Replace(",",".")}," +
                                       $"RestaurantKosten = {invoice.Restaurant_Costs.ToString().Replace(",",".")}," +
                                       $"Kunden_Rabatt_Wert = {invoice.Customer_Discount_Value.ToString().Replace(",",".")}," +
                                       $"Angebotsaktion_Wert = {invoice.Specialoffers_Value.ToString().Replace(",",".")}," +
                                       $"Steuersumme = {invoice.Tax_Sum.ToString().Replace(",",".")} WHERE ID = {invoice.ID}", db.connection);
            cmd.ExecuteNonQuery();
            return cmd.LastInsertedId;
        }
        catch (Exception e)
        {
            MessageBox.Show("Der Besuch konnte nicht aktualisiert werden\n" + "Error: " +e.Message);
        }

        return 0;
    }
    
    public static Invoice GetInvoiceById(int id)
    {
        using var db = new Database.Database();
            
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
            var cmd = new MySqlCommand($"SELECT * FROM `rechnung` WHERE ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new Invoice(id, reader.GetInt32(1), reader.GetDouble(2), 
                    reader.GetDouble(3), reader.GetDouble(4), reader.GetDouble(5),
                    reader.GetDouble(6), reader.GetDouble(7), reader.GetInt32(8),
                    reader.GetDouble(9), reader.GetBoolean(10), reader.GetBoolean(11));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Es konnte kein Besuch gefunden werden\n" + ex.Message);
            return null;
        }

        return null;
    }
}