using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace KiranaStoreUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public ProductController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        // ✅ Helper: Creates HttpClient with JWT automatically
        private HttpClient CreateClientWithToken()
        {
            var client = _factory.CreateClient("api");
            var token = HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        // LIST
        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public async Task<IActionResult> Index()
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<List<Product>>("Product/GetProducts");
            return View(data);
        }


        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var client = CreateClientWithToken();
            var p = await client.GetFromJsonAsync<Product>($"Product/GetProduct/{id}");
            if (p == null) return NotFound();
            return View(p);
        }

        // CREATE (GET)
        public async Task<IActionResult> Create()
         {
            await LoadCategories();
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(Product model)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return View(model);
            }

            var client = CreateClientWithToken();
            var result = await client.PostAsJsonAsync("Product/AddProduct", model);

            if (!result.IsSuccessStatusCode)
            {
                string apiResponse = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError("", apiResponse);
                await LoadCategories();
                return View(model);
            }
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var client = CreateClientWithToken();
            var p = await client.GetFromJsonAsync<Product>($"Product/GetProduct/{id}");
            if (p == null) return NotFound();

            await LoadCategories();
            return View(p);
        }

        // EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(Product model)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategories();
                return View(model);
            }

            var client = CreateClientWithToken();
            var result = await client.PutAsJsonAsync("Product/UpdateProduct", model);

            if (result.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Failed to update product.");
            await LoadCategories();
            return View(model);
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var client = CreateClientWithToken();
            var p = await client.GetFromJsonAsync<Product>($"Product/GetProduct/{id}");
            if (p == null) return NotFound();

            return View(p);
        }

        // DELETE CONFIRMED
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = CreateClientWithToken();
            await client.DeleteAsync($"Product/DeleteProduct/{id}");
            return RedirectToAction("Index");
        }

        // Load categories
        private async Task LoadCategories()
        {
            var client = CreateClientWithToken();
            var categories = await client.GetFromJsonAsync<List<Category>>("Category/GetCategories");

            ViewBag.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();
        }
    }
}
