using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant.Data.MySQL
{
    internal class MySQLUtil
    {
        private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static MySqlConnection? GetMySQLConnection()
        {
            MySqlConnection? connection = null;
            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
            return connection;
        }

        public static void Close(MySqlConnection connection)
        {
            connection?.Close();
        }

        public static void Close(MySqlDataReader reader)
        {
            reader?.Close();
        }

        public static void Close(MySqlDataReader reader, MySqlConnection connection)
        {
            Close(reader);
            Close(connection);
        }
    }
}
