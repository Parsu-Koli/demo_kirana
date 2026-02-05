namespace KiranaStoreUI.Models
{
    public class AuditLog
    {
        public int AuditId { get; set; }
        public string Action { get; set; }
        public string PerformedBy { get; set; }
        public DateTime ActionDate { get; set; }
    }
}
