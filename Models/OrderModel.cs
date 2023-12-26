using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hci_restaurant.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public int TableId { get; set; }
        public string UserUsername { get; set; }
    }
}
