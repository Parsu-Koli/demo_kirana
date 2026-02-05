using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IHttpClientFactory _factory;

        public OrderItemController(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        private HttpClient CreateClientWithToken()
        {
            var client = _factory.CreateClient("api");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public async Task<IActionResult> Index()
        {
            var client = CreateClientWithToken();
            var orders = await client.GetFromJsonAsync<List<OrderItem>>("OrderItem/GetOrderItems");
            return View(orders);
        }

        public async Task<IActionResult> GetById(int orderId)
        {
            var client = CreateClientWithToken();
            var data = await client.GetFromJsonAsync<List<OrderItem>>($"OrderItem/Get/{orderId}");
            ViewBag.OrderId = orderId;
            return View(data);
        }

        public IActionResult Create(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem model)
        {
            var client = CreateClientWithToken();

            var response = await client.PostAsJsonAsync("OrderItem/Add", model);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Stock insufficient or sale failed");
                return View(model);
            }

            return RedirectToAction("Index", "Product");
        }
    }
}
