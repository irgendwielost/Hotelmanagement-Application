using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.ViewModels.TypesOfStay;

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
}