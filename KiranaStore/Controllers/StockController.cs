using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly StockService _stockService;

        public StockController(StockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost("IncreaseStock")]
        public IActionResult IncreaseStock(int productId, int qty)
        {
            try
            {
                _stockService.IncreaseStock(productId, qty);
                return Ok("Stock increased successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DecreaseStock")]
        public IActionResult DecreaseStock(int productId, int qty)
        {
            try
            {
                _stockService.DecreaseStock(productId, qty);
                return Ok("Stock decreased successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllStock")]
        public IActionResult GetAllStock()
        {
            try
            {
                var data = _stockService.GetAllStock();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStockByProduct/{productId}")]
        public IActionResult GetStockByProduct(int productId)
        {
            try
            {
                var stock = _stockService.GetStockByProductId(productId);
                if (stock == null)
                    return NotFound("Stock not found");

                return Ok(stock);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetLowStock/{limit}")]
        public IActionResult GetLowStock(int limit)
        {
            try
            {
                var data = _stockService.GetLowStock(limit);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
