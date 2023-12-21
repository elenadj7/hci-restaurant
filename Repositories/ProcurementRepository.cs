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
    public class ProcurementRepository : IProcurementRepository
    {
        public int AddProcurement(string username)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddProcurement", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@user_username_", MySqlDbType.String).Value = username;
                    command.Parameters.Add("@id_", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    int id = Convert.ToInt32(command.Parameters["@id_"].Value);
                    return id;
                }
            }
        }

        public void AddItemsToProcurement(string username, ItemModel item, decimal purchasePrice, int quantity)
        {
            int procurementId = AddProcurement(username);
            AddProcurementHasItem(procurementId, item, purchasePrice, quantity);
        }

        public void AddProcurementHasItem(int procurementId, ItemModel item, decimal purchasePrice, int quantity)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddProcurementHasItem", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@procurement_id_", MySqlDbType.Int32).Value = procurementId;
                    command.Parameters.Add("@item_id_", MySqlDbType.Int32).Value = item.Id;
                    command.Parameters.Add("@quantity_", MySqlDbType.Int32).Value = quantity;
                    command.Parameters.Add("@purchase_price_", MySqlDbType.Decimal).Value = purchasePrice;
                    command.ExecuteNonQuery();

                }
            }
        }

        public ObservableCollection<ProcurementModel> GetAllByUser(string username)
        {
            ObservableCollection<ProcurementModel> procurements = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetAllProcurements", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@user_username_", MySqlDbType.String).Value = username;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProcurementModel procurement = new()
                            {
                                Id = reader.GetInt32(0),
                                UserUsername = reader.GetString(1),
                                IsFinished = reader.GetInt16(2),
                                Ordered = reader.GetDateTime(3),
                            };

                            if (!reader.IsDBNull(4))
                            {
                                procurement.Arrived = reader.GetDateTime(4);
                            }

                            procurements.Add(procurement);
                        }
                    }
                }
            }

            return procurements;
        }

        public ObservableCollection<ProcurementHasItemModel> GetItemDataByUsernameAndProcurementId(string username, int procurementId)
        {
            ObservableCollection<ProcurementHasItemModel> items = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetItemDataByUsernameAndProcurementId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.Parameters.Add("@procurement_id_", MySqlDbType.Int32).Value = procurementId;
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

                            ProcurementHasItemModel item = new()
                            {
                                ProcurementId = reader.GetInt32(1),
                                Item = i,
                                Quantity = reader.GetInt32(6),
                                PurchasePrice = reader.GetDecimal(7),
                            };

                            

                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }

        public void DeleteProcurement(int id)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("DeleteProcurement", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id_", MySqlDbType.Int32).Value = id;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
