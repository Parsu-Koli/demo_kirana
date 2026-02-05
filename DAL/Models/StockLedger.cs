using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class StockLedger
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string TransactionType { get; set; }  // Purchase / Sale
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }

}
