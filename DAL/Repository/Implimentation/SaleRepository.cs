//using DAL.Data;
//using DAL.Models;
//using DAL.Repository.Interfaces;
//using System.Collections.Generic;
//using System.Linq;

//namespace DAL.Repository.Implementation
//{
//    public class SaleRepository : ISaleRepository
//    {
//        private readonly AppDbContext _context;

//        public SaleRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        public void Add(Sale sale)
//        {
//            _context.Sales.Add(sale);
//            _context.SaveChanges();
//        }

//        public Sale GetById(int id)
//        {
//            return _context.Sales.Find(id);
//        }

//        public IEnumerable<Sale> GetAll()
//        {
//            return _context.Sales.ToList();
//        }

//        public IEnumerable<Sale> GetByDate(DateTime date)
//        {
//            return _context.Sales.Where(x => x.SaleDate.Date == date.Date).ToList();
//        }
//    }
//}

using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Implementation
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;

        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Sale sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();
        }

        public Sale GetById(int id)
        {
            var sale = _context.Sales.Find(id);

            if (sale != null)
                LoadSaleItems(sale);   // Auto-load sale items

            return sale;
        }

        public IEnumerable<Sale> GetAll()
        {
            return _context.Sales
                           .OrderByDescending(s => s.SaleId)
                           .ToList();
        }

        public IEnumerable<Sale> GetByDate(DateTime date)
        {
            return _context.Sales
                           .Where(x => x.SaleDate.Date == date.Date)
                           .OrderByDescending(x => x.SaleId)
                           .ToList();
        }

        // 🔥 PUBLIC METHOD because interface requires it
        public void LoadSaleItems(Sale sale)
        {
            _context.Entry(sale)
                    .Collection(s => s.SaleItems)
                    .Load();
        }
    }
}
