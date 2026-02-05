using KiranaStoreUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace KiaranaStroreUI.Controllers
{
    public class AuditLogsController : Controller
    {
        private readonly HttpClient _client;

        public AuditLogsController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("api");
        }

        public async Task<IActionResult> Index()
        {
            var data = await _client.GetFromJsonAsync<List<AuditLog>>("AuditLogs");
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await _client.GetFromJsonAsync<AuditLog>($"AuditLogs/{id}");
            return View(data);
        }
    }
}
