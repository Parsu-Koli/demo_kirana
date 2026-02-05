using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiranaStoreUI.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly HttpClient _client;

        public PurchaseController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        // ----------- LIST ALL PURCHASES ------------
        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<Purchase>>("Purchase/GetPurchases");
            return View(data);
        }

        // ----------- CREATE PAGE ------------
        public IActionResult Create() => View();

        // ----------- CREATE POST ------------
        [HttpPost]
        public async Task<IActionResult> Create(Purchase model)
        {
            var result = await _client.PostAsJsonAsync("Purchase/AddPurchase", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // ----------- EDIT PAGE ------------
        public async Task<IActionResult> Edit(int id)
        {
            var data = await _client.GetFromJsonAsync<Purchase>($"Purchase/GetPurchase/{id}");
            return View(data);
        }

        // ----------- EDIT POST ------------
        [HttpPost]
        public async Task<IActionResult> Edit(Purchase model)
        {
            // Assuming you add UpdatePurchase endpoint later
            var result = await _client.PutAsJsonAsync($"Purchase/UpdatePurchase/{model.PurchaseId}", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // ----------- DELETE PAGE ------------
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _client.GetFromJsonAsync<Purchase>($"Purchase/GetPurchase/{id}");
            return View(data);
        }

        // ----------- DELETE CONFIRMED ------------
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.DeleteAsync($"Purchase/DeletePurchase/{id}");
            return RedirectToAction("Index");
        }
    }
}
