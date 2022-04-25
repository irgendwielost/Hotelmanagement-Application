using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.ViewModels.Department
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
    }
}