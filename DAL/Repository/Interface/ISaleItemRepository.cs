using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface ISaleItemRepository
    {
        IEnumerable<SaleItem> GetItemsBySale(int saleId);
        void Add(SaleItem item);
        IEnumerable<SaleItem> GetAll();
    }
}
