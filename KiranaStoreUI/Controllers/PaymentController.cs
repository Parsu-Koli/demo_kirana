using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HttpClient _client;

        public PaymentController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public IActionResult Create(int saleId)
        {
            // Pre-fill SaleId + Today's Date
            var model = new Payment
            {
                SaleId = saleId,
                PaymentDate = DateTime.Now
            };

            return View(model);
        }


        public async Task<IActionResult> Index()
        {
            var result = await _client.GetFromJsonAsync<List<Payment>>("Payment/GetPayments");
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Payment model)
        {
            var result = await _client.PostAsJsonAsync("Payment/AddPayment", model);

            if (result.IsSuccessStatusCode)
            {
                TempData["msg"] = "Payment Completed Successfully!";
                return RedirectToAction("Create", new { saleId = model.SaleId });
            }

            TempData["error"] = "Failed to add payment";
            return View(model);
        }
    }
}
