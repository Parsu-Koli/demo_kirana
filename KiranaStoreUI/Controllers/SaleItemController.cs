using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class SaleItemController : Controller
    {
        private readonly HttpClient _client;

        public SaleItemController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<SaleItem>>("SaleItem/GetAllSaleItems");
            return View(data);
        }

        public async Task<IActionResult> Details(int saleId)
        {
            var items = await _client.GetFromJsonAsync<List<SaleItem>>($"SaleItem/GetItems/{saleId}");
            ViewBag.SaleId = saleId;
            return View(items);
        }

      
        public IActionResult Create(int saleId)
        {
            var model = new SaleItem
            {
                SaleId = saleId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaleItem model)
        {
            var result = await _client.PostAsJsonAsync("SaleItem/Add", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Details", new { saleId = model.SaleId });

            return View(model);
        }
    }
}
