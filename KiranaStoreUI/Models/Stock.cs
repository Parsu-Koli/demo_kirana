//namespace KiranaStoreUI.Models
//{
//    public class Stock
//    {
//        public int StockId { get; set; }        // Primary Key

//        public int ProductId { get; set; }      // Foreign Key to Product
//        public int Quantity { get; set; }       // Current stock count

//        public int MinimumQuantity { get; set; }  // For low stock alert

//        // Navigation (optional, safe)


//        public Product Product { get; set; }
//    }
//}

using System.ComponentModel.DataAnnotations;

namespace KiranaStoreUI.Models
{
    public class Stock
    {
        [Key]
        public int StockId { get; set; }  // Primary Key

        [Required(ErrorMessage = "Product is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0")]
        public int ProductId { get; set; }  // Foreign Key to Product

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }  // Current stock count

        [Required(ErrorMessage = "Minimum Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum Quantity cannot be negative")]
        public int MinimumQuantity { get; set; }  // For low stock alert

        // Navigation property
        public Product Product { get; set; }
    }
}
