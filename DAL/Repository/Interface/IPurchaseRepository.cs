using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface IPurchaseRepository
    {
        Purchase GetById(int id);
        IEnumerable<Purchase> GetAll();
        void Add(Purchase purchase);
    }
}
