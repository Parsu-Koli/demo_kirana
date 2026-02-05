using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class SupplierController : Controller
    {
        private readonly HttpClient _client;

        public SupplierController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        // ========== LIST ==========
        public async Task<IActionResult> Index()
        {
            var suppliers = await _client.GetFromJsonAsync<List<Supplier>>("Supplier/GetAll");
            return View(suppliers);
        }

        // ========== DETAILS ==========
        public async Task<IActionResult> Details(int id)
        {
            var supplier = await _client.GetFromJsonAsync<Supplier>($"Supplier/Get/{id}");
            return View(supplier);
        }

        // ========== CREATE ==========
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Supplier model)
        {
            var result = await _client.PostAsJsonAsync("Supplier/Add", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // ========== EDIT ==========
        public async Task<IActionResult> Edit(int id)
        {
            var supplier = await _client.GetFromJsonAsync<Supplier>($"Supplier/Get/{id}");
            return View(supplier);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Supplier model)
        {
            var result = await _client.PutAsJsonAsync("Supplier/Update", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // ========== DELETE ==========
        public async Task<IActionResult> Delete(int id)
        {
            var supplier = await _client.GetFromJsonAsync<Supplier>($"Supplier/Get/{id}");
            return View(supplier);
        }

    }
}