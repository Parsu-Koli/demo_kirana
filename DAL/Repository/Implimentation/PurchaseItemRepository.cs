using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Implementation
{
    public class PurchaseItemRepository : IPurchaseItemRepository
    {
        private readonly AppDbContext _context;

        public PurchaseItemRepository(AppDbContext context)
        {
            _context = context;
        }


        public IEnumerable<PurchaseItem> GetAll()
        {
            return _context.PurchaseItems.ToList();
        }


        public void Add(PurchaseItem item)
        {
            _context.PurchaseItems.Add(item);
            _context.SaveChanges();
        }

        public IEnumerable<PurchaseItem> GetItemsByPurchase(int purchaseId)
        {
            return _context.PurchaseItems.Where(x => x.PurchaseId == purchaseId).ToList();
        }
    }
}
