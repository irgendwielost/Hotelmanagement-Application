using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.Salary
{
    public class SalaryDB
    {
        public static DataSet GetSalary()
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
                
                var adapter = new MySqlDataAdapter("select * from `einkommen`", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "einkommen");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}