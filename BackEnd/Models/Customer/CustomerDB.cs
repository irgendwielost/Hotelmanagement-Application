using System;
using System.Data;
using System.Windows;
using Hotelmanagement.BackEnd.ViewModels.CustomerPaymentmethods;
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
        
        public static long CreateCustomer(Customer customer)
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
                var cmd = new MySqlCommand($"INSERT INTO `kunden` (ID, Name, Geburtstag," +
                                           $"Telefon, EMail, Strasse, Wohnort, Plz, Entfernt) VALUES ({customer.ID}, " +
                                           $"'{customer.Name}', ?Geburtstag,'{customer.Telephone}', '{customer.Email}', " +
                                           $"'{customer.Street}', '{customer.Place}', " +
                                           $"'{customer.PostalCode}', false)", db.connection);
                cmd.Parameters.Add("?Geburtstag", MySqlDbType.Date).Value = customer.Birthday;
                cmd.ExecuteNonQuery();
                return cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Es konnte kein neuer Kunde erstellt werden\n" + "Error:" + ex.Message);
            }

            return 0;
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
        
        public static Customer GetCustomerById(int id)
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
                var cmd = new MySqlCommand($"SELECT * FROM `kunden` WHERE ID={id}", db.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new Customer(id, reader.GetString(1), reader.GetDateTime(2), 
                        reader.GetString(3), reader.GetString(4), reader.GetString(5),
                        reader.GetString(6), reader.GetString(7),
                        reader.GetBoolean(8));
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
    }
}