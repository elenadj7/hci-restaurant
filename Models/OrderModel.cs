using System;

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
