using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Implementation
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _context;

        public PurchaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Purchase purchase)
        {
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
        }

        public Purchase GetById(int id)
        {
            return _context.Purchases.Find(id);
        }

        public IEnumerable<Purchase> GetAll()
        {
            return _context.Purchases.ToList();
        }
    }
}
