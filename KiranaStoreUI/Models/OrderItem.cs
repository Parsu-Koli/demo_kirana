namespace KiranaStoreUI.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }    // Primary Key

        public int OrderId { get; set; }        // FK to Order
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }      // Product Price
        public decimal Total { get; set; }      // Quantity * Price

  
    }
}
