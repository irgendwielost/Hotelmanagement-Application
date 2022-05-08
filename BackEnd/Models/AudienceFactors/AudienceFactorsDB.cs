using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.AudienceFactors
{
    public class AudienceFactorsDB
    {
        public static DataSet GetDataSetAudienceFactors()
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
                
                var adapter = new MySqlDataAdapter("select * from `zielgruppenfaktoren`", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "zielgruppenfaktoren");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    
        public static void CreateCustomerTAF(AudienceFactors taf)
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
                var cmd = new MySqlCommand($"INSERT INTO `zielgruppenfaktoren` (Kunde_ID, Geschlecht," +
                                           $" Anreisegrund, Familienstand, Unternehmen, Beruf, Lebensstil, Einkommen," +
                                           $" Geschäftsreise, Entfernt) VALUES ({taf.CustomerID}, '{taf.Gender}', " +
                                           $" '{taf.TravelReason}', '{taf.Familystate}'," +
                                           $" '{taf.Company}', '{taf.Job}', '{taf.Lifestyle}', '{taf.Salary}'," +
                                           $" {taf.IsBusinesstrip}, false)", db.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dem Kunden konnten keine Zielgruppenfaktoren hinzugefügt werden\n" 
                                + "Error:" + ex.Message);
            }
        }
    }
}