using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Data;

namespace hci_restaurant.Repositories
{
    public class TableRepository : ITableRepository
    {
        public int AddTable(int seats)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@seats_number_", MySqlDbType.Int32).Value = seats;
                    command.Parameters.Add("@id_", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    int id = Convert.ToInt32(command.Parameters["@id_"].Value);
                    return id;
                }
            }
        }

        public void DeleteTable(int id)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("DeleteTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id_", MySqlDbType.Int32).Value = id;
                    command.ExecuteNonQuery();
                }
            }
        }

        public ObservableCollection<TableModel> GetAll()
        {
            ObservableCollection<TableModel> tables = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetAllTables", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableModel table = new()
                            {
                                Id = reader.GetInt32(0),
                                Seats = reader.GetInt32(1)
                            };

                            tables.Add(table);
                        }
                    }
                }
            }

            return tables;
        }

        public ObservableCollection<TableModel> GetAllByFilter(int seats)
        {
            ObservableCollection<TableModel> tables = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetTablesByFilter", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@filter_", MySqlDbType.Int32).Value = seats;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TableModel table = new()
                            {
                                Id = reader.GetInt32(0),
                                Seats = reader.GetInt32(1)
                            };

                            tables.Add(table);
                        }
                    }
                }
            }

            return tables;
        }

        public void UpdateTable(int id, int seats)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("UpdateTable", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@id_", MySqlDbType.Int32).Value = id;
                    command.Parameters.Add("@seats_number_", MySqlDbType.Int32).Value = seats;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
