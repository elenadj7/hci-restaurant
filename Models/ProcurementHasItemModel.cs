namespace hci_restaurant.Models
{
    public class ProcurementHasItemModel
    {
        public int ProcurementId { get; set; }
        public ItemModel Item {  get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
    }
}
