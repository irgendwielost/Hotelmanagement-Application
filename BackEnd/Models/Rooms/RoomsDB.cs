using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Rooms
{
    public class RoomsDB
    {
        public static DataSet GetDataSetRooms()
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
                
                var adapter = new MySqlDataAdapter("select * from `zimmer`", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "zimmer");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public static Rooms GetRoomById(int id)
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
                var cmd = new MySqlCommand($"SELECT * FROM `zimmer` WHERE ID={id}", db.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Rooms(id, reader.GetString(1), reader.GetString(2), 
                        reader.GetDouble(3), reader.GetInt32(4), reader.GetString(5),
                        reader.GetInt32(6), reader.GetDouble(7),
                        reader.GetString(8), reader.GetBoolean(9), reader.GetDateTime(10));
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
        public static void CreateRoom(Rooms room)
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
                var cmd = new MySqlCommand($"INSERT INTO `zimmer` (Bezeichnung, Groesse, Standartpreis, " +
                                           $"Anzahl, Beschreibung, Zustellbettkapazität, Zustellbettpreis, " +
                                           $"Lage, Entfernt) VALUES ('{room.Designation}', '{room.Size}', {room.BasePrice}, " +
                                           $"{room.Amount}, '{room.Description}', {room.ExtraBedCapacity}, " +
                                           $"{room.ExtraBedPrice}, '{room.Situation}', false)", db.connection);
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