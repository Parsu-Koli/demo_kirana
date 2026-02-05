using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Implementation
{
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly AppDbContext _context;

        public SaleItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(SaleItem item)
        {
            _context.SaleItems.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<SaleItem> GetAll()
        {
            return _context.SaleItems.ToList();
        }
        public IEnumerable<SaleItem> GetItemsBySale(int saleId)
        {
            return _context.SaleItems.Where(x => x.SaleId == saleId).ToList();
        }
    }
}
