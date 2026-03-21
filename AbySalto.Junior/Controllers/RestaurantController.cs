using AbySalto.Junior.Common;
using Application.DTOs;
using Application.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public RestaurantController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders([FromQuery] bool sortByTotal = false)
        {
            var result = await _orderService.GetAllOrders(sortByTotal);
            return StatusHandler.HandleResult(this, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);
            return StatusHandler.HandleResult(this, result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] PostOrderDTO dto)
        {
            var result = await _orderService.AddOrder(dto);
            return StatusHandler.HandleResult(this, result);
        }


        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] int orderStatusId)
        {
            var result = await _orderService.UpdateOrderStatus(id, orderStatusId);
            return StatusHandler.HandleResult(this, result);
        }

    }
}
