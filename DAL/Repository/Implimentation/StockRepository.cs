using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Implementation
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
        }

        public void Update(Stock stock)
        {
            _context.Stocks.Update(stock);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.Stocks.Find(id);
            if (obj != null)
            {
                _context.Stocks.Remove(obj);
                _context.SaveChanges();
            }
        }

        public Stock GetById(int id)
        {
            return _context.Stocks.Find(id);
        }

        public Stock GetByProductId(int productId)
        {
            return _context.Stocks.FirstOrDefault(x => x.ProductId == productId);
        }

        public IEnumerable<Stock> GetAll()
        {
            return _context.Stocks.ToList();
        }

        public IEnumerable<Stock> GetLowStock(int limit)
        {
            return _context.Stocks.Where(x => x.Quantity <= limit).ToList();
        }

        public void IncreaseStock(int productId, int qty)
        {
            var stock = GetByProductId(productId);
            if (stock != null)
            {
                stock.Quantity += qty;
                Update(stock);
            }
        }

        public void DecreaseStock(int productId, int qty)
        {
            var stock = GetByProductId(productId);
            if (stock != null)
            {
                stock.Quantity -= qty;
                Update(stock);
            }
        }
    }
}
