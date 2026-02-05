using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AuditLog
    {
        public int AuditId { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public DateTime ActionDate { get; set; }
    }

}
