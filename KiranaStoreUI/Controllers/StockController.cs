
using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class StockController : Controller
    {
        private readonly HttpClient _client;

        public StockController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<Stock>>("Stock");
            return View(data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Stock model)
        {
            var result = await _client.PostAsJsonAsync("Stock", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _client.GetFromJsonAsync<Stock>($"Stock/{id}");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Stock model)
        {
            var result = await _client.PutAsJsonAsync($"Stock/{model.StockId}", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _client.GetFromJsonAsync<Stock>($"Stock/{id}");
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.DeleteAsync($"Stock/{id}");
            return RedirectToAction("Index");
        }
    }
}
