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

        public UserModel AuthenticateUser(string username, SecureString password)
        {
            MySqlConnection conn = null;
            try
            {
                string plainPassword = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(System.Runtime.InteropServices.Marshal.SecureStringToBSTR(password));
                conn = RepositoryBase.GetConnection();
                MySqlCommand cmd = new MySqlCommand("GetUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@username_", MySqlDbType.String).Value = username;
                cmd.Parameters.Add("@password_", MySqlDbType.String).Value = plainPassword;
                cmd.Parameters.Add("@status_", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@name", MySqlDbType.String).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@surname", MySqlDbType.String).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@salary", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@role", MySqlDbType.Int16).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

                int status = 0;
                if (cmd.Parameters["@status_"].Value != DBNull.Value)
                {
                    status = Convert.ToInt32(cmd.Parameters["@status_"].Value);
                }

                if (status == 1)
                {
                    UserModel user = new()
                    {
                        //TODO: fix 
                        Username = username,
                        Name = cmd.Parameters["@name"].Value?.ToString(),
                        Surname = cmd.Parameters["@surname"].Value?.ToString(),
                        Salary = Convert.ToInt32(cmd.Parameters["@salary"].Value),
                        Role = Convert.ToInt16(cmd.Parameters["@role"].Value)
                    };

                    return user;
                }

                return null;

            }
            catch (Exception e)
            {
                //TODO
                //MessageBox.Show(e.Message);
                throw new Exception();
            }
            finally
            {
                RepositoryBase.Close(conn);
            }
        }

        public UserModel GetUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
