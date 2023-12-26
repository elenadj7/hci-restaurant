using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using MySqlConnector;
using System.Collections.ObjectModel;
using System.Data;

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
                                Quantity = reader.GetInt32(4),
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

        public void AddItem(ItemModel item, int categoryId)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@name_", MySqlDbType.String).Value = item.Name;
                    command.Parameters.Add("@price_", MySqlDbType.Decimal).Value = item.Price;
                    command.Parameters.Add("@description_", MySqlDbType.String).Value = item.Description;
                    command.Parameters.Add("@quantity_", MySqlDbType.Int16).Value = item.Quantity;
                    command.Parameters.Add("@category_id_", MySqlDbType.Int32).Value = categoryId;

                    command.ExecuteNonQuery();
                }
            }
        }

        public ObservableCollection<ItemModel> GetAllByCategory(int categoryId)
        {
            ObservableCollection<ItemModel> items = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetItemsByCategory", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@category_id", MySqlDbType.Int32).Value = categoryId;
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
                                Quantity = reader.GetInt32(4),
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

        public void UpdateItem(int id, decimal price, int quantity)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("UpdateItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id_", MySqlDbType.Int32).Value = id;
                    command.Parameters.Add("@quantity_", MySqlDbType.Int32).Value = quantity;
                    command.Parameters.Add("@price_", MySqlDbType.Decimal).Value = price;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
