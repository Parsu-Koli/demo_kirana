using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface IStockRepository
    {
        Stock GetById(int id);
        IEnumerable<Stock> GetAll();
        IEnumerable<Stock> GetLowStock(int limit);

        void Add(Stock stock);
        void Update(Stock stock);
        void Delete(int id);

        // Stock Management
        void IncreaseStock(int productId, int qty);
        void DecreaseStock(int productId, int qty);
        Stock GetByProductId(int productId);
    }
}
