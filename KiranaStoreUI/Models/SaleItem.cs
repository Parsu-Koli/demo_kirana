//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

//namespace KiranaStoreUI.Models
//{
//    public class SaleItem
//    {
//        public int SaleItemId { get; set; }
//        public int SaleId { get; set; }
//        public int ProductId { get; set; }
//        public int Quantity { get; set; }
//        public decimal Price { get; set; }         
//        public decimal Total { get; set; }

//        [ValidateNever]
//        public Sale Sale { get; set; }
//        [ValidateNever]
//        public Product Product { get; set; }

//    }
//}


using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace KiranaStoreUI.Models
{
    public class SaleItem
    {
        [Key]
        public int SaleItemId { get; set; }

        [Required(ErrorMessage = "Sale is required")]
        [Range(1, int.MaxValue, ErrorMessage = "SaleId must be greater than 0")]
        public int SaleId { get; set; }

        [Required(ErrorMessage = "Product is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ProductId must be greater than 0")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total cannot be negative")]
        public decimal Total { get; set; }  // Can be auto-calculated

        [ValidateNever]
        public Sale Sale { get; set; }

        [ValidateNever]
        public Product Product { get; set; }
    }
}
