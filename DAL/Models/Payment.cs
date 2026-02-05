using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int SaleId { get; set; }
        public decimal AmountPaid { get; set; }
        public string Mode { get; set; }         
        public DateTime PaymentDate { get; set; }
    }

}
