using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using DAL.Models;

namespace KiranaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseItemController : ControllerBase
    {
        private readonly PurchaseItemService _service;

        public PurchaseItemController(PurchaseItemService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public IActionResult Add(PurchaseItem item)
        {
            try
            {
                _service.AddPurchaseItem(item);
                return Ok("Purchase Item Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetItems/{purchaseId}")]
        public IActionResult GetItems(int purchaseId)
        {
            return Ok(_service.GetItemsByPurchase(purchaseId));
        }

        [HttpGet("GetAllPurchaseitem")]
        public IActionResult GetAllPurchaseitem()
        {
            return Ok(_service.GetAllPurchaseitem());
        }

    }
}
