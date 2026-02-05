namespace KiranaStoreUI.Models.ViewModels
{
    public class SaleProfitVM
    {
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }          // NetAmount
        public decimal PurchasePrice { get; set; }  // Total purchase cost of items
        public decimal SellingPrice { get; set; }   // Total selling price
        public decimal TotalDiscount { get; set; }  // Discount applied
        public DateTime SaleDate { get; set; }
        public decimal Profit => SellingPrice - PurchasePrice - TotalDiscount;
    }

}
