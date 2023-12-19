using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace hci_restaurant.Repositories
{
    public class ItemRepository : IItemRepository
    {
        public ObservableCollection<ItemModel> GetAll()
        {
            ObservableCollection<ItemModel> items = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetAllItems", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemModel item = new()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Description = "/",
                                Available = reader.GetInt16(4),
                                Category = reader.GetString(5)
                            };

                            if (!reader.IsDBNull(3))
                            {
                                item.Description = reader.GetString(3);
                            }
                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        public void DeleteItem(int id)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("DeleteItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id_", MySqlDbType.Int32).Value = id;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
