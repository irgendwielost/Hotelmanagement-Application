using System;
using System.Windows;
using Hotelmanagement.BackEnd.Database;
using Hotelmanagement.BackEnd.ViewModels.VisitRoom;
using MySql.Data.MySqlClient;

public class VisitRoomDB
{
    public static void AddVisitroom(VisitRoom visitRoom)
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
            var cmd = new MySqlCommand($"INSERT INTO `besuch-zimmer` (Besuch_ID, Zimmer_ID, Zustellbett, " +
                                       $"`Zustellbett-Anzahl`) VALUES ({visitRoom.Visit_id}, {visitRoom.Room_id}, " +
                                       $"{visitRoom.Extra_bed}, {visitRoom.Extra_bed_amount})",db.connection);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Es konnte kein Zimmer zu dem Besuch gebucht werden\n" + "Error: " + ex.Message);
        }
    }
}