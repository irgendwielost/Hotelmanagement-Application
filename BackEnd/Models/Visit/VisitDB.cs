using System;
using System.Data;
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
    }
}