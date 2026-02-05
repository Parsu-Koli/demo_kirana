using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface ISupplierRepository
    {
        Supplier GetById(int id);
        IEnumerable<Supplier> GetAll();
        void Add(Supplier supplier);
        void Update(Supplier supplier);
        void Delete(int id);
    }
}
