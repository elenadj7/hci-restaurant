using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hci_restaurant.Repositories
{
    public class RepositoryBase
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static MySqlConnection? GetConnection()
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

        protected static void Close(MySqlDataReader reader)
        {
            reader?.Close();
        }

        protected static void Close(MySqlDataReader reader, MySqlConnection connection)
        {
            Close(reader);
            Close(connection);
        }
    }
}
