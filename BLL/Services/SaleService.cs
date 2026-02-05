using DAL.Models;
using DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class SaleService
    {
        private readonly ISaleRepository _saleRepo;
        private readonly IProductRepository _productRepo; 

        public SaleService(ISaleRepository saleRepo, IProductRepository productRepo)
        {
            _saleRepo = saleRepo;
            _productRepo = productRepo;
        }

        
        public void AddSale(Sale sale)
        {
            if (sale.CustomerId <= 0)
                throw new Exception("Select customer.");

            if (sale.SaleItems == null || sale.SaleItems.Count == 0)
                throw new Exception("Add at least one product.");

            sale.SaleDate = DateTime.Now;
            sale.InvoiceNumber = GenerateInvoiceNumber();

            _saleRepo.Add(sale);
        }

        // ---------------- GET ALL SALES (with items + products) ----------------
        public IEnumerable<Sale> GetAllSales()
        {
            var sales = _saleRepo.GetAll().ToList();

            foreach (var sale in sales)
            {
                _saleRepo.LoadSaleItems(sale);       // load sale items
                LoadProductsForSaleItems(sale);      // load product info
            }

            return sales;
        }

        // ---------------- GET SINGLE SALE (with items + products) ----------------
        public Sale GetSale(int saleId)
        {
            var sale = _saleRepo.GetById(saleId);
            if (sale == null) return null;

            _saleRepo.LoadSaleItems(sale);           // load sale items
            LoadProductsForSaleItems(sale);          // load product info

            return sale;
        }

        // ---------------- HELPER: Load products for each SaleItem ----------------
        private void LoadProductsForSaleItems(Sale sale)
        {
            if (sale.SaleItems == null || sale.SaleItems.Count == 0) return;

            var productIds = sale.SaleItems.Select(si => si.ProductId).ToList();
            var products = _productRepo.GetAll()
                                       .Where(p => productIds.Contains(p.ProductId))
                                       .ToDictionary(p => p.ProductId, p => p);

            foreach (var item in sale.SaleItems)
            {
                if (products.ContainsKey(item.ProductId))
                    item.Product = products[item.ProductId];
            }
        }

        // ---------------- INVOICE NUMBER ----------------
        public string GetNextInvoiceNumber()
        {
            return GenerateInvoiceNumber();
        }

        private string GenerateInvoiceNumber()
        {
            var lastSale = _saleRepo.GetAll()
                                    .OrderByDescending(s => s.SaleId)
                                    .FirstOrDefault();

            if (lastSale == null || string.IsNullOrEmpty(lastSale.InvoiceNumber))
                return "INV000001";

            string numericPart = lastSale.InvoiceNumber.Substring(3);
            int lastNumber = int.Parse(numericPart);
            int newNumber = lastNumber + 1;

            return "INV" + newNumber.ToString("000000");
        }
    }
}

