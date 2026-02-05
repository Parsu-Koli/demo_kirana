using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiranaStoreUI.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }      // Primary Key

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Customer ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Customer ID must be greater than 0")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Total amount is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
    }
}
