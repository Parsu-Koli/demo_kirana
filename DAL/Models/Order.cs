using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Order
    {
        public int OrderId { get; set; }      // Primary Key

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerId { get; set; }   
  
        public decimal TotalAmount { get; set; }

        // Navigation (safe)
        [JsonIgnore]
        public List<OrderItem>? Items { get; set; }
    }
}
