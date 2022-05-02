using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Visit
{
    public class VisitDB
    {
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