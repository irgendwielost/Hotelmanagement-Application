using System;
using System.Data;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.Models.Invoice;
using MySql.Data.MySqlClient;

public class InvoiceDB
{
    public static DataSet GetDataSetInvoice()
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
            var cmd = new MySqlCommand($"INSERT INTO `rechnung` (Besuch_ID, Besuch_Gesamtkosten, Zimmer_ID," +
                                       $"Zimmer_Kosten, ServiceKosten, RestaurantKosten, Kunden_Rabatt_Wert, " +
                                       $"Angebotsaktion_Wert, Steuer_ID, Steuersumme, ErstelltAm,Bezahlt, Entfernt) " +
                                       $"VALUES ({invoice.VisitID}, " +
                                       $"{invoice.VisitTotalCosts.ToString().Replace(",",".")}, " +
                                       $"{invoice.Room_Id}," +
                                       $"{invoice.Room_Costs.ToString().Replace(",",".")}," +
                                       $"{invoice.Service_Costs.ToString().Replace(",",".")}, " +
                                       $"{invoice.Restaurant_Costs.ToString().Replace(",",".")}, " +
                                       $"{invoice.Customer_Discount_Value}, {invoice.Specialoffers_Value}, {invoice.Tax_Id}," +
                                       $"{invoice.Tax_Sum.ToString().Replace(",",".")}, @CreatedAt," +
                                       $"{invoice.Paid}, {invoice.Deleted})", db.connection);
            cmd.Parameters.Add("@CreatedAt", MySqlDbType.Date).Value = invoice.Created_At;
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
            var cmd = new MySqlCommand($"UPDATE `rechnung` SET " +
                                       $"Besuch_Gesamtkosten = {invoice.VisitTotalCosts.ToString().Replace(",",".")}," +
                                       $"Zimmer_Kosten = {invoice.Room_Costs.ToString().Replace(",",".")}," +
                                       $"ServiceKosten = {invoice.Service_Costs.ToString().Replace(",",".")}," +
                                       $"RestaurantKosten = {invoice.Restaurant_Costs.ToString().Replace(",",".")}," +
                                       $"Kunden_Rabatt_Wert = {invoice.Customer_Discount_Value.ToString().Replace(",",".")}," +
                                       $"Angebotsaktion_Wert = {invoice.Specialoffers_Value.ToString().Replace(",",".")}," +
                                       $"Steuersumme = {invoice.Tax_Sum.ToString().Replace(",",".")}, " +
                                       $"ErstelltAm = @CreatedAt " +
                                       $"WHERE ID = {invoice.ID}", db.connection);
            cmd.Parameters.Add("@CreatedAt", MySqlDbType.Date).Value = invoice.Created_At;
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
            var cmd = new MySqlCommand($"SELECT * FROM `rechnung` WHERE ID={id}", db.connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                return new Invoice(id, reader.GetInt32(1), reader.GetDouble(2), reader.GetInt32(3),
                    reader.GetDouble(4), reader.GetDouble(5), reader.GetDouble(6),
                    reader.GetDouble(7), reader.GetDouble(8), reader.GetInt32(9),
                    reader.GetDouble(10), reader.GetDateTime(11), reader.GetBoolean(12), 
                    reader.GetBoolean(13));
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Es konnte kein Besuch gefunden werden\n" + ex.Message);
            return null;
        }

        return null;
    }
    
    public static DataSet DataSetInvoice()
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
            var adapter = new MySqlDataAdapter($"select rechnung.* , " +
                                               "k.Name as CustomerName, " +
                                               "z.Bezeichnung as RoomName from `rechnung` " +
                                               "join besuche b on rechnung.Besuch_ID = b.ID " +
                                               "join kunden k on b.Kunde_ID = k.ID " +
                                               "join zimmer z on rechnung.Zimmer_ID = z.ID WHERE Bezahlt = 0", db.connection);
            DataSet dataSet = new();
            adapter.Fill(dataSet, "rechnung");
            return dataSet;
        }
        catch
        {
            return null;
        }
    }

    public static void BookInvoice(int id)
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
            var cmd = new MySqlCommand($"UPDATE `rechnung` SET Bezahlt = 1 WHERE ID = {id}", db.connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            MessageBox.Show("Die Rechnung konnte nicht abgeschlossen werden\n" + "Error: " +e.Message);
        }
    }
}