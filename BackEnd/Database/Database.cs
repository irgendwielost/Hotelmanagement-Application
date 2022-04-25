using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Hotelmanagement.BackEnd.Database
{
    public class Database : IDisposable
    {
        public MySqlConnection connection { get; }
        //Function to connect to the database

        public Database()
        {
            try
            {
                connection = new MySqlConnection(
                    new MySqlConnectionStringBuilder
                    {
                        Server = "sql651.your-server.de",
                        UserID = "lostra_school",
                        Password = "Friday366",
                        Database = "hotel_management"
                    }.ConnectionString);
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Es konnte keine Verbindung zu der Datenbank hergestellt werden:");
                MessageBox.Show("Es konnte keine Verbindung zu der Datenbank hergestellt werden!");
                Debug.WriteLine(e);
                throw;
            }

            connection.Close();
        }

        //Function to execute a query
        public void ExecuteQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        //Function to return a DataTable
        public DataTable ReturnDataTable(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}