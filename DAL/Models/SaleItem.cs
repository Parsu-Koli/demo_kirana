//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace DAL.Models
//{
//    public class SaleItem
//    {
//        public int SaleItemId { get; set; }
//        public int SaleId { get; set; }
//        public int ProductId { get; set; }
//        public int Quantity { get; set; }
//        public decimal Price { get; set; }         // SellingPrice
//        public decimal Total { get; set; }
//        [JsonIgnore]
//        public Sale? Sale { get; set; }
//        [JsonIgnore]
//        public Product? Product { get; set; }
//    }

//}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }
        public int SaleId { get; set; }        // FK to Sale
        public int ProductId { get; set; }     // FK to Product
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }     // SellingPrice
        public decimal Total { get; set; }

        [JsonIgnore]
        [ForeignKey("SaleId")]
        public Sale? Sale { get; set; }

        [JsonIgnore]
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
    }
}
