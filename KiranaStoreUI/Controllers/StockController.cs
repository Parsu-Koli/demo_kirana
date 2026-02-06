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

        public async Task<IActionResult> Index()
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<List<Stock>>("Stock/GetAllStock");
            return View(data);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Stock model)
        {
            var client = CreateClientWithToken();
            var result = await client.PostAsJsonAsync("Stock/IncreaseStock?productId=" + model.ProductId + "&qty=" + model.Quantity, model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Chart()
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<List<Stock>>("Stock/GetAllStock");
            return View(data);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<Stock>($"Stock/GetStockByProduct/{id}");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Stock model)
        {
            var client = CreateClientWithToken();
            var result = await client.PostAsJsonAsync("Stock/IncreaseStock?productId=" + model.ProductId + "&qty=" + model.Quantity, model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<Stock>($"Stock/GetStockByProduct/{id}");
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = CreateClientWithToken();
            await client.DeleteAsync($"Stock/Delete/{id}");
            return RedirectToAction("Index");
        }
    }
}
