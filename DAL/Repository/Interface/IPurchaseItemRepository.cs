using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface IPurchaseItemRepository
    {
        IEnumerable<PurchaseItem> GetItemsByPurchase(int purchaseId);
        void Add(PurchaseItem item);
        IEnumerable<PurchaseItem> GetAll();
    }
}
