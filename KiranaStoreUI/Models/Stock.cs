namespace KiranaStoreUI.Models
{
    public class Stock
    {
        public int StockId { get; set; }        // Primary Key

        public int ProductId { get; set; }      // Foreign Key to Product
        public int Quantity { get; set; }       // Current stock count

        public int MinimumQuantity { get; set; }  // For low stock alert

        // Navigation (optional, safe)
        public Product Product { get; set; }
    }
}
