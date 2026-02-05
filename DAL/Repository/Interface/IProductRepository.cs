using DAL.Models;

namespace DAL.Repository.Interface
{
    public interface IProductRepository
    {
        Product GetById(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetByCategory(int categoryId);

        void Add(Product product);
        void Update(Product product);
        void Delete(int id);

        bool IsProductNameExists(string name);

        void DecreaseProductStock(int productId, int qty);
        void IncreaseProductStock(int productId, int qty);

    }
}
