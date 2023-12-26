using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Data;

namespace hci_restaurant.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public ObservableCollection<CategoryModel> GetAll()
        {
            ObservableCollection<CategoryModel> categories = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetAllCategories", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CategoryModel category = new()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                            };

                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        }
    }
}
