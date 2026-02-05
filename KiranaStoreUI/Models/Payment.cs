namespace KiranaStoreUI.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int SaleId { get; set; }
        public decimal AmountPaid { get; set; }
        public string Mode { get; set; }         // UPI, Cash, Card
        public DateTime PaymentDate { get; set; }
    }
}
