using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.Models
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


        public List<SaleItem> SaleItems { get; set; }
    }

}
