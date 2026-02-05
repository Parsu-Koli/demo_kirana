using KiranaStoreUI.Models;
using KiranaStoreUI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace KiranaStoreUI.Controllers
{
    public class SaleController : Controller
    {
        private readonly HttpClient _client;

        public SaleController(IHttpClientFactory factory)
        {
            // Get configured HttpClient from factory
            _client = factory.CreateClient("api");
        }

        // ✅ Helper: Attach JWT token to HttpClient
        private void AddJwtToken()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        // ---------------- INDEX ----------------
        public async Task<IActionResult> Index()
        {
            AddJwtToken();

            var sales = await _client.GetFromJsonAsync<List<Sale>>("Sale/GetAllSales");
            var customers = await _client.GetFromJsonAsync<List<Customer>>("Customer/GetCustomers");

            var customerDict = customers.ToDictionary(c => c.CustomerId, c => c.Name);

            foreach (var s in sales)
            {
                s.Customer = new Customer
                {
                    Name = s.CustomerId.HasValue && customerDict.ContainsKey(s.CustomerId.Value)
                           ? customerDict[s.CustomerId.Value]
                           : "N/A"
                };
            }

            return View(sales);
        }

        // ---------------- CREATE PAGE ----------------
        public async Task<IActionResult> Create()
        {
            AddJwtToken();

            string nextInvoice = await _client.GetStringAsync("Sale/GetNextInvoice");

            var vm = new SaleCustomerVM
            {
                Sale = new Sale
                {
                    InvoiceNumber = nextInvoice,
                    SaleDate = DateTime.Now,
                    SaleItems = new List<SaleItem>() // initialize empty list
                },
                Customer = new Customer()
            };

            ViewBag.NextInvoice = nextInvoice;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleCustomerVM vm)
        {
            AddJwtToken();

            if (vm.Sale == null || vm.Customer == null)
            {
                ModelState.AddModelError("", "Sale or Customer data is missing.");
                return View(vm);
            }

            // Step 1: Create Customer
            var custResponse = await _client.PostAsJsonAsync("Customer/AddCustomer", vm.Customer);
            if (!custResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Customer creation failed.");
                ViewBag.NextInvoice = vm.Sale.InvoiceNumber;
                return View(vm);
            }

            var createdCustomer = await custResponse.Content.ReadFromJsonAsync<Customer>();
            if (createdCustomer == null || createdCustomer.CustomerId <= 0)
            {
                ModelState.AddModelError("", "Invalid customer data returned from server.");
                ViewBag.NextInvoice = vm.Sale.InvoiceNumber;
                return View(vm);
            }

            vm.Sale.CustomerId = createdCustomer.CustomerId;

            // Step 2: Clean SaleItems
            if (vm.Sale.SaleItems != null)
            {
                foreach (var item in vm.Sale.SaleItems)
                    item.Sale = null;
            }

            // Step 3: Create Sale
            var saleResponse = await _client.PostAsJsonAsync("Sale/AddSale", vm.Sale);
            if (saleResponse.IsSuccessStatusCode)
            {
                TempData["SuccessMsg"] = $"Sale created successfully! Invoice: {vm.Sale.InvoiceNumber}";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sale creation failed.");
            ViewBag.NextInvoice = vm.Sale.InvoiceNumber;
            return View(vm);
        }

        // ---------------- SEARCH PRODUCT ----------------
        [HttpGet]
        public async Task<JsonResult> SearchProduct(string keyword)
        {
            AddJwtToken();

            if (string.IsNullOrWhiteSpace(keyword))
                return Json(new { success = false, products = new List<object>() });

            var result = await _client.GetAsync($"Sale/SearchProducts?keyword={keyword}");
            if (!result.IsSuccessStatusCode)
                return Json(new { success = false });

            var products = await result.Content.ReadFromJsonAsync<List<Product>>();
            return Json(new { success = true, products });
        }

        // ---------------- DETAILS ----------------
        public async Task<IActionResult> Details(int id)
        {
            AddJwtToken();

            var sale = await _client.GetFromJsonAsync<Sale>($"Sale/GetSale/{id}");
            if (sale == null) return NotFound();

            if (sale.SaleItems != null && sale.SaleItems.Count > 0)
            {
                var productIds = sale.SaleItems.Select(si => si.ProductId).ToList();
                var products = await _client.GetFromJsonAsync<List<Product>>("Product/GetProducts");
                var productDict = products.Where(p => productIds.Contains(p.ProductId))
                                          .ToDictionary(p => p.ProductId, p => p);

                foreach (var item in sale.SaleItems)
                {
                    if (productDict.ContainsKey(item.ProductId))
                        item.Product = productDict[item.ProductId];
                }
            }

            return View(sale);
        }

        // ---------------- EDIT ----------------
        public async Task<IActionResult> Edit(int id)
        {
            AddJwtToken();
            var data = await _client.GetFromJsonAsync<Sale>($"Sale/GetSale/{id}");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Sale model)
        {
            AddJwtToken();

            if (model.SaleItems != null)
            {
                foreach (var item in model.SaleItems)
                    item.Sale = null;
            }

            var result = await _client.PutAsJsonAsync($"Sale/UpdateSale/{model.SaleId}", model);
            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }
    }
}
