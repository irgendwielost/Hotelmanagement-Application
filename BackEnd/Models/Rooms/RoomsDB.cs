using System;
using System.Data;
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

        public static void GetDescriptionRoom(int id)
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
                
                var cmd = new MySqlCommand("select Beschreibung from `zimmer`", db.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
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
                //Insert into method
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}