using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiranaStoreUI.Controllers
{
    public class PurchaseItemController : Controller
    {
        private readonly HttpClient _client;

        public PurchaseItemController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

       
        public async Task<IActionResult> Details(int purchaseId)
        {
            var data = await _client.GetFromJsonAsync<List<PurchaseItem>>(
                        $"PurchaseItem/GetItems/{purchaseId}");

            ViewBag.PurchaseId = purchaseId;

            return View(data);
        }


        public async Task<IActionResult> Index()
        {
            var result = await _client.GetFromJsonAsync<List<PurchaseItem>>("PurchaseItem/GetAllPurchaseitem");
            return View(result);
        }

        public IActionResult Create(int purchaseId)
        {
            var model = new PurchaseItem
            {
                PurchaseId = purchaseId
            };

            return View(model);
        }

        // ---------------- CREATE POST ----------------
        [HttpPost]
        public async Task<IActionResult> Create(PurchaseItem model)
        {
            model.Total = model.Quantity * model.Price;

            var result = await _client.PostAsJsonAsync("PurchaseItem/Add", model);
            

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index", new { purchaseId = model.PurchaseId });

            return View(model);
        }
    }
}
