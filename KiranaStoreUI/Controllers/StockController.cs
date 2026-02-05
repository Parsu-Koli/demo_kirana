using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class StockController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public StockController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        // ✅ Helper: Create HttpClient with JWT
        private HttpClient CreateClientWithToken()
        {
            var client = _factory.CreateClient("api");
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        // LIST
        public async Task<IActionResult> Index()
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<List<Stock>>("Stock/GetAllStock");
            return View(data);
        }

        // CREATE (GET)
        public IActionResult Create() => View();

        // CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Stock model)
        {
            var client = CreateClientWithToken();
            var result = await client.PostAsJsonAsync("Stock/IncreaseStock?productId=" + model.ProductId + "&qty=" + model.Quantity, model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // CHART
        public async Task<IActionResult> Chart()
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<List<Stock>>("Stock/GetAllStock");
            return View(data);
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<Stock>($"Stock/GetStockByProduct/{id}");
            return View(data);
        }

        // EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Stock model)
        {
            var client = CreateClientWithToken();
            var result = await client.PostAsJsonAsync("Stock/IncreaseStock?productId=" + model.ProductId + "&qty=" + model.Quantity, model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<Stock>($"Stock/GetStockByProduct/{id}");
            return View(data);
        }

        // DELETE CONFIRMED
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = CreateClientWithToken();
            await client.PostAsync($"Stock/DecreaseStock?productId={id}&qty=0", null);
            return RedirectToAction("Index");
        }
    }
}
