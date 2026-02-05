using DAL.Models;
using DAL.Repository.Interfaces;

namespace BLL.Services
{
    public class SupplierService
    {
        private readonly ISupplierRepository _supplierRepo;

        public SupplierService(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        public void AddSupplier(Supplier supplier)
        {
            if (string.IsNullOrWhiteSpace(supplier.SupplierName))
                throw new Exception("Supplier name required.");

            if (string.IsNullOrWhiteSpace(supplier.Phone))
                throw new Exception("Supplier mobile required.");

            _supplierRepo.Add(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            if (supplier.SupplierId <= 0)
                throw new Exception("Invalid supplier ID.");

            _supplierRepo.Update(supplier);
        }

        public void DeleteSupplier(int id)
        {
            if (id <= 0)
                throw new Exception("Invalid ID.");

            _supplierRepo.Delete(id);
        }

        public Supplier GetSupplier(int id)
        {
            return _supplierRepo.GetById(id);
        }

        public IEnumerable<Supplier> GetAllSuppliers()
        {
            return _supplierRepo.GetAll();
        }
    }
}
