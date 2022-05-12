using System;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Models.TypesOfStay
{
    public class TypesOfStayDB
    {
        public static DataSet GetDataSetTypesOfStay()
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
                
                var adapter = new MySqlDataAdapter("select * from `aufenthaltsarten`", db.connection);
                DataSet dataSet = new();
                adapter.Fill(dataSet, "aufenthaltsarten");
                return dataSet;
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }
        
        public static ViewModels.TypesOfStay.TypesOfStay GetTypeOfStayByID(int id)
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
                var cmd = new MySqlCommand($"SELECT * FROM `aufenthaltsarten` WHERE ID={id}", db.connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    return new ViewModels.TypesOfStay.TypesOfStay(id, reader.GetString(1), 
                        reader.GetDouble(2), reader.GetBoolean(3));
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            return null;
        }
    }
}