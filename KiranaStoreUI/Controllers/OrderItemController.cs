using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly HttpClient _client;

        public OrderItemController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        // Show Items of an Order
        public async Task<IActionResult> Index()
        {
            var orders = await _client.GetFromJsonAsync<List<OrderItem>>("OrderItem/GetOrderItems");
            return View(orders);
        }

        public async Task<IActionResult> GetById(int orderId)
        {
            var data = await _client.GetFromJsonAsync<List<OrderItem>>($"OrderItem/Get/{orderId}");
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
            var response = await _client.PostAsJsonAsync("OrderItem/Add", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", new { orderId = model.OrderId });

            return View(model);
        }
    }
}
