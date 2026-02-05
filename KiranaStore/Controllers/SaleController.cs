using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using DAL.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace KiranaStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _saleService;
        private readonly ProductService _productService;

        public SaleController(SaleService saleService, ProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
        }

        // ---------------- SEARCH PRODUCTS ----------------
        [HttpGet("SearchProducts")]
        public IActionResult SearchProducts(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Ok(new List<Product>());

            var products = _productService.SearchProducts(keyword).ToList();
            return Ok(products);
        }

        // ---------------- ADD SALE ----------------
        [HttpPost("AddSale")]
        public IActionResult AddSale(Sale sale)
        {
            try
            {
                // Null navigation properties to avoid EF Core issues
                if (sale.SaleItems != null)
                {
                    foreach (var item in sale.SaleItems)
                    {
                        item.Sale = null; // EF will link via SaleId
                    }
                }

                _saleService.AddSale(sale);
                return Ok("Sale Added Successfully");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // ---------------- GET SALE BY ID (With Items + Product) ----------------
        [HttpGet("GetSale/{id}")]
        public IActionResult GetSale(int id)
        {
            var sale = _saleService.GetSale(id);

            if (sale == null)
                return NotFound("Sale not found");

            // Optional: populate product names for each SaleItem
            if (sale.SaleItems != null)
            {
                foreach (var item in sale.SaleItems)
                {
                    if (item.ProductId > 0 && item.Product == null)
                    {
                        item.Product = _productService.GetProduct(item.ProductId);
                    }
                }
            }

            return Ok(sale);
        }

        // ---------------- GET ALL SALES (With Items) ----------------
        [HttpGet("GetAllSales")]
        public IActionResult GetAllSales()
        {
            var sales = _saleService.GetAllSales();

            // Optional: populate product names for each SaleItem
            foreach (var sale in sales)
            {
                if (sale.SaleItems != null)
                {
                    foreach (var item in sale.SaleItems)
                    {
                        if (item.ProductId > 0 && item.Product == null)
                        {
                            item.Product = _productService.GetProduct(item.ProductId);
                        }
                    }
                }
            }

            return Ok(sales);
        }

        // ---------------- GET NEXT INVOICE ----------------
        [HttpGet("GetNextInvoice")]
        public string GetNextInvoice()
        {
            return _saleService.GetNextInvoiceNumber();
        }
    }
}
