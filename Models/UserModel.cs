using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname {  get; set; }
        public int Salary { get; set; }
        public short Role {  get; set; }
        public override string ToString()
        {
            return Username;
        }
    }
}
