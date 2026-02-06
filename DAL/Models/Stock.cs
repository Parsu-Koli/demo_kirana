namespace DAL.Models
{
    public class Stock
    {
        public int StockId { get; set; }        // Primary Key

        public int ProductId { get; set; }      // Foreign Key to Product
        public decimal Quantity { get; set; }       // Current stock count

        public decimal MinimumQuantity { get; set; }  // For low stock alert

        // Navigation (optional, safe)
        public Product Product { get; set; }
    }
}
