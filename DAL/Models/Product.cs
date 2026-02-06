using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        public required int ProductId { get; set; }
        public required int CategoryId { get; set; }
        
        [NotMapped]
        public string? CategoryName { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required string Unit { get; set; }         // Kg, Litre, Pack, Piece
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellingPrice { get; set; }
        public required decimal QuantityInStock { get; set; }
        public required bool Active { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }
    }

}
