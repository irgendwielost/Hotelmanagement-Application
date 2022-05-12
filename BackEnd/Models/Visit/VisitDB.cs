using System;
using System.Data;
using System.Globalization;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Visit
{
    public class VisitDB
    {
        public static DataSet GetDataSetVisits()
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
                
                var adapter = new MySqlDataAdapter("select besuche.* , k.Name as CustomerName, a.Bezeichnung as TypesOfStayName, z.Bezeichnung as RoomName from `besuche` join kunden k on k.ID = besuche.Kunde_ID join aufenthaltsarten a on a.ID = besuche.Besuchsart_ID join zimmer z on besuche.Zimmer_ID = z.ID WHERE Abgeschlossen = 0", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "besuche");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public static void UpdateVisit(Visit visit)
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
                var cmd = new MySqlCommand($"UPDATE `besuche` SET " +
                                           $"Servicekosten = {visit.Service_Costs}, " +
                                           $"Zimmerkosten = {visit.Room_Costs}, " +
                                           $"Gesamtkosten = {visit.Total_Costs.ToString().Replace(",",".")}, " +
                                           $"SpeiseKosten = {visit.Dish_Costs}, " +
                                           $"Kunden_Rabatt = {visit.Customer_Discount}," +
                                           $"Angebotsaktion = {visit.Special_Offer}, " +
                                           $"Abgeschlossen = true WHERE ID={visit.ID}", 
                    db.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show("Der Besuch konnte nicht aktualisiert werden\n" + "Error: " +e.Message);
            }
        }
        
        public static Visit GetVisitById(int id)
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
                var cmd = new MySqlCommand($"SELECT * FROM `besuche` WHERE ID={id}", db.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Visit(id, reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3),
                        reader.GetInt32(4), reader.GetDouble(5), reader.GetDouble(6),
                        reader.GetDateTime(7), reader.GetDateTime(8), reader.GetDouble(9),
                        reader.GetBoolean(10), reader.GetString(11), 
                        reader.GetDouble(12), reader.GetBoolean(13),
                        reader.GetBoolean(14),reader.GetBoolean(15));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es konnte kein Besuch gefunden werden\n" + ex.Message);
                return null;
            }

            return null;
        }
        public static int GetRoomsInVisitsById(int id)
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
                using var cmd = new MySqlCommand($"SELECT COUNT(ID) FROM `besuche` " +
                                                 $"WHERE Zimmer_ID={id} AND Abgeschlossen = 0", db.connection);
                Int32 count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Es ist ein Fehler aufgetreten");
            }

            return 0;
        }
        
        public static long AddVisit(Visit visit)
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
                var cmd = new MySqlCommand($"INSERT INTO `besuche` (ID, Kunde_ID, Besuchsart_ID, Zimmer_ID," +
                                           $" Personenanzahl, Servicekosten, Zimmerkosten, Ankunft, Abfahrt," +
                                           $" Gesamtkosten, Reklamiert, Reklamationsgrund, SpeiseKosten," +
                                           $" Kunden_Rabatt, Angebotsaktion, Abgeschlossen) VALUES  ({visit.ID}, " +
                                           $"{visit.Customer_ID}, {visit.Visit_Type_Of_Stay_ID},{visit.Room_ID}, {visit.Person_Amount}, " +
                                           $"{visit.Service_Costs}, {visit.Room_Costs}, @Arrival, " +
                                           $"@Departure, {visit.Total_Costs.ToString().Replace(",",".")}, " +
                                           $"{visit.Complained}, " +
                                           $"'{visit.Complain_Reason}', {visit.Dish_Costs}, {visit.Customer_Discount}," +
                                           $"{visit.Special_Offer}, false)",db.connection);
                cmd.Parameters.Add("@Arrival", MySqlDbType.Date).Value = visit.Arrival;
                cmd.Parameters.Add("@Departure", MySqlDbType.Date).Value = visit.Departure;
                cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es konnte kein Besuch gebucht werden\n" + "Error: " + ex.Message);
            }

            return 0;
        }
        
        public static DataSet GetDataSetVisitForInvoice(int id)
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
                
                var adapter = new MySqlDataAdapter("select besuche.* , " +
                                                   "k.Name as CustomerName, " +
                                                   "z.Bezeichnung as RoomName from `besuche` " +
                                                   "join kunden k on k.ID = besuche.Kunde_ID " +
                                                   "join zimmer z on besuche.Zimmer_ID = z.ID " +
                                                   $"WHERE ID={id}", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "besuche");
                return dataSet;
            }
            catch
            {
                return null;
            }
        }
    }
}