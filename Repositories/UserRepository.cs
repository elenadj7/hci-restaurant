using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Security;

namespace hci_restaurant.Repositories
{
    class UserRepository : IUserRepository
    {
        public UserModel? AuthenticateUser(string username, SecureString password)
        {
            string plainPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password));
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.Parameters.Add("@password_", MySqlDbType.String).Value = plainPassword;
                    command.Parameters.Add("@status_", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        int status = Convert.ToInt32(command.Parameters["@status_"].Value);

                        if (status == 1)
                        {
                            if (reader.Read())
                            {
                                UserModel user = new()
                                {
                                    Username = username,
                                    Name = reader.GetString(2),
                                    Surname = reader.GetString(3),
                                    Salary = reader.GetInt32(4),
                                    Role = reader.GetInt16(5)
                                };

                                return user;
                            }
                        }

                        return null;
                    }
                }
            }
        }

        public string? GetTheme(string username)
        {
            using(MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using(MySqlCommand command = new MySqlCommand("GetTheme", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }

            return null;
        }

        public string? GetLanguage(string username)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetLanguage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }

            return null;
        }

        public void UpdateTheme(string username, string theme)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("UpdateTheme", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.Parameters.Add("@newTheme_", MySqlDbType.String).Value = theme;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateLanguage(string username, string language)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("UpdateLanguage", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.Parameters.Add("@newLanguage_", MySqlDbType.String).Value = language;
                    command.ExecuteNonQuery();
                }
            }
        }

        public ObservableCollection<UserModel> GetAll()
        {
            ObservableCollection<UserModel> users = new ();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new()
                            {
                                Username = reader.GetString(0),
                                Name = reader.GetString(2),
                                Surname = reader.GetString(3),
                                Salary = reader.GetInt32(4),
                                Role = reader.GetInt16(5)
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public void DeleteUser(string username)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddUser(UserModel user, string password)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("AddUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = user.Username;
                    command.Parameters.Add("@password_", MySqlDbType.String).Value = password;
                    command.Parameters.Add("@name_", MySqlDbType.String).Value = user.Name;
                    command.Parameters.Add("@surname_", MySqlDbType.String).Value = user.Surname;
                    command.Parameters.Add("@salary_", MySqlDbType.Int32).Value = user.Salary;

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (MySqlException e)
                    {
                        throw;
                    }
                }
            }
        }

        public ObservableCollection<UserModel> GetAllByFilter(string filter)
        {
            ObservableCollection<UserModel> users = new();
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("FindUsersByFilter", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@filter_", MySqlDbType.String).Value = filter;
                    command.ExecuteNonQuery();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new()
                            {
                                Username = reader.GetString(0),
                                Name = reader.GetString(2),
                                Surname = reader.GetString(3),
                                Salary = reader.GetInt32(4),
                                Role = reader.GetInt16(5)
                            };

                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }

        public void UpdateUser(string username, string name, string surname, int salary)
        {
            using (MySqlConnection connection = RepositoryBase.GetConnection())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                    command.Parameters.Add("@name_", MySqlDbType.String).Value = name;
                    command.Parameters.Add("@surname_", MySqlDbType.String).Value = surname;
                    command.Parameters.Add("@salary_", MySqlDbType.Int32).Value = salary;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
