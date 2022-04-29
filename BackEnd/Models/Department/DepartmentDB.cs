using System;
using System.Data;
using System.Data.Common;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Department
{
    public class DepartmentDB
    {
        public static DataSet GetDataSetDepartment()
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
                
                var adapter = new MySqlDataAdapter("select * from abteilung", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "abteilung");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
        
        public static void CreateDepartment(ViewModels.Department.Department department)
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
                var cmd = new MySqlCommand($"INSERT INTO abteilung (Name, Entfernt) VALUES ('{department.Name}', 0)", db.connection);
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