using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiranaStoreUI.Models
{
    public class Order
    {
        public int OrderId { get; set; }      // Primary Key

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }    
        public decimal TotalAmount { get; set; }

    }
}
