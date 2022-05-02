using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Service
{
    public class ServiceDB
    {
        public static DataSet GetDataSetService()
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
                
                var adapter = new MySqlDataAdapter("select * from `serviceangebote`", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "serviceangebote");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        
        public static Service GetServiceById(int id)
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
                var cmd = new MySqlCommand($"SELECT * FROM `serviceangebote` WHERE ID={id}", db.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Service(id, reader.GetString(1), reader.GetString(2), 
                        reader.GetDouble(3), reader.GetInt32(4), 
                        reader.GetInt32(5), reader.GetBoolean(6));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Es ist ein Fehler aufgetreten");
                return null;
            }

            return null;
        }
        
        public static void AddService(Service service)
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
                var cmd = new MySqlCommand($"INSERT INTO `serviceangebote` (ID, Bezeichnung, Beschreibung, Preis, " +
                                           $"Kategorie_ID, `Dauer-Minuten`, Entfernt) VALUES ({service.ID},'{service.Designation}', '{service.Description}', " +
                                           $"{service.Price}, " +
                                           $"{service.Category_id}, {service.Duration_in_minutes}, false) " +
                                           $"ON DUPLICATE KEY UPDATE Bezeichnung = '{service.Designation}', " +
                                           $"Beschreibung = '{service.Description}', Preis =  {service.Price}, " +
                                           $"Kategorie_ID = {service.Category_id}, `Dauer-Minuten` = {service.Duration_in_minutes}, " +
                                           $"Entfernt = false", db.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Es ist ein Fehler aufgetreten");
            }
        }
    }
}