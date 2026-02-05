namespace KiranaStoreUI.Models
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }         // SellingPrice
        public decimal Total { get; set; }

        public Sale Sale { get; set; }
        public Product Product { get; set; }

    }
}
