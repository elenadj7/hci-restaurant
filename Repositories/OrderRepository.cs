using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public int AddOrder(int tableId, string userUsername)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@table_id_", MySqlDbType.Int32).Value = tableId;
                    command.Parameters.Add("@user_username_", MySqlDbType.String).Value = userUsername;
                    command.Parameters.Add("@id_", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    int id = Convert.ToInt32(command.Parameters["@id_"].Value);
                    return id;
                }
            }
        }

        public int AddOrderWithNote(int tableId, string note, string userUsername)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@table_id_", MySqlDbType.Int32).Value = tableId;
                    command.Parameters.Add("@user_username_", MySqlDbType.String).Value = userUsername;
                    command.Parameters.Add("@note_", MySqlDbType.String).Value = note;
                    command.Parameters.Add("@id_", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    int id = Convert.ToInt32(command.Parameters["@id_"].Value);
                    return id;
                }
            }
        }

        public ObservableCollection<OrderModel> GetAllByUsername(string username)
        {
            ObservableCollection<OrderModel> orders = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetOrdersByUsername", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_username_", MySqlDbType.String).Value = username;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            OrderModel order = new()
                            {
                                Id = reader.GetInt32(0),
                                Created = reader.GetDateTime(1),
                                TableId = reader.GetInt32(2),
                                UserUsername = reader.GetString(3)
                            };

                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }

        public void DeleteOrder(int orderId)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("DeleteOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id_", MySqlDbType.Int32).Value = orderId;
                    command.ExecuteNonQuery();
                }
            }
        }

        public ObservableCollection<OrderHasItemModel> GetItemDataByUsernameAndOrderId(string username, int orderId)
        {
            ObservableCollection<OrderHasItemModel> items = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetItemDataByUsernameAndOrderId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.Parameters.Add("@order_id_", MySqlDbType.Int32).Value = orderId;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemModel i = new()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(2),
                                Description = reader.GetString(4),
                                Price = reader.GetDecimal(3),
                                Category = reader.GetString(5),
                            };

                            OrderHasItemModel item = new()
                            {
                                OrderId = reader.GetInt32(1),
                                Item = i,
                                Quantity = reader.GetInt32(6),
                            };



                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        public void AddOrderHasItem(int orderId, ItemModel item, int quantity)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddOrderHasItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@order_id_", MySqlDbType.Int32).Value = orderId;
                    command.Parameters.Add("@item_id_", MySqlDbType.Int32).Value = item.Id;
                    command.Parameters.Add("@quantity_", MySqlDbType.Int32).Value = quantity;
                    command.ExecuteNonQuery();

                }
            }
        }

        public void DeleteOrderHasItem(int orderId, int itemId)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("DeleteOrderHasItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@order_id_", MySqlDbType.Int32).Value = orderId;
                    command.Parameters.Add("@item_id_", MySqlDbType.Int32).Value = itemId;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
