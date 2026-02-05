using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace KiranaStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _service;

        public OrderItemController(OrderItemService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public IActionResult Add(OrderItem item)
        {
            try
            {
                _service.AddOrderItem(item);
                return Ok("Order Item Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Get/{orderId}")]
        public IActionResult Get(int orderId)
        {
            return Ok(_service.GetItemsByOrder(orderId));
        }

        [HttpGet("GetOrderItems")]
        public IActionResult GetOrderItems()
        {
            return Ok(_service.GetAllOrderItems());
        }
    }
}
