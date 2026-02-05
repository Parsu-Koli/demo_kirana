using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiranaStoreUI.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public int CategoryId { get; set; }

       
        [BindNever]
        [NotMapped]
        public string? CategoryName { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string? Description { get; set; }

        [Required]
        public string Unit { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }

        public int QuantityInStock { get; set; }

        public bool Active { get; set; } = true;
    }
}
