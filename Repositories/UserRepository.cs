using hci_restaurant.Models;
using hci_restaurant.Models.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace hci_restaurant.Repositories
{
    class UserRepository : IUserRepository
    {

        public IEnumerable<UserModel> GetAll()
        {
            throw new NotImplementedException();
        }

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

        public UserModel GetUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
