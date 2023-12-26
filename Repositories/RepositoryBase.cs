using MySqlConnector;
using System.Configuration;

namespace hci_restaurant.Repositories
{
    public class RepositoryBase
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
