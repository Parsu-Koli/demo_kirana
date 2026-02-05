namespace KiranaStoreUI.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public string PaymentMode { get; set; }      // Cash, UPI, Card
        public DateTime SaleDate { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }

        public List<SaleItem>? SaleItems { get; set; }
    }
}
