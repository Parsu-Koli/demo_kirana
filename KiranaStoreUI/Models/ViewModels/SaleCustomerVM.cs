namespace KiranaStoreUI.Models.ViewModels
{
    public class SaleCustomerVM
    {
        public Sale Sale { get; set; }
        public Customer Customer { get; set; }

        public SaleCustomerVM()
        {
            Sale = new Sale();
            Customer = new Customer();
        }
    }
}
