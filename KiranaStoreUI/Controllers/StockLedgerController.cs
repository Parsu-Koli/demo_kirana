
using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class StockLedgerController : Controller
    {
        private readonly HttpClient _client;

        public StockLedgerController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<StockLedger>>("StockLedger");
            return View(data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(StockLedger model)
        {
            var result = await _client.PostAsJsonAsync("StockLedger", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await _client.GetFromJsonAsync<StockLedger>($"StockLedger/{id}");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StockLedger model)
        {
            var result = await _client.PutAsJsonAsync($"StockLedger/{model.Id}", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var data = await _client.GetFromJsonAsync<StockLedger>($"StockLedger/{id}");
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.DeleteAsync($"StockLedger/{id}");
            return RedirectToAction("Index");
        }
    }
}
