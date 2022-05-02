using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Customer
{
    public class CustomerDB
    {
        public static DataSet GetDataSetCustomer()
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
                
                var adapter = new MySqlDataAdapter("select * from `kunden`", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "kunden");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        
        public static void CreateCustomer(Customer customer)
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
                var cmd = new MySqlCommand($"INSERT INTO `kunden` (ID, Name, " +
                                           $"Telefon, EMail, Strasse, Wohnort, Plz, Entfernt) VALUES ({customer.ID}, " +
                                           $"'{customer.Name}', '{customer.Telephone}', '{customer.Email}', " +
                                           $"'{customer.Street}', '{customer.Place}', " +
                                           $"'{customer.PostalCode}', false)", db.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es konnte kein neuer Kunde erstellt werden\n" + "Error:" + ex.Message);
            }
        }

        public static void DeleteCustomer(int id)
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
                var cmd = new MySqlCommand($"UPDATE `kunden` SET Entfernt = true WHERE ID={id}", db.connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Der Kunde konnte nicht gelöscht werden\n" + "Error:" + ex.Message);
            }
        }
    }
}