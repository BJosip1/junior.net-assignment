using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Domain.Models;

namespace AbySalto.Junior.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        public RestaurantController(RestaurantController _restaurantRepository)
        {

        }

        [HttpGet("orders")]
        public IEnumerable<Order> GetAllOrders()
        {
            return Enumerable.Empty<Order>();
        }

        [HttpGet("orders/{id}")]
        public Order GetOrderById()
        {
            return new Order();
        }

        [HttpPost("new")]
        public int AddOrder([FromBody] PostOrderDTO newOrder)
        {
            return 0;
        }
        [HttpPut("edit")]
        public int EditOrder([FromBody] Order order)
        {
            return 0;
        }
        [HttpDelete("delete/{id}")]
        public int DeleteOrder([FromRoute] int id)
        {
            return 0;
        }
    }
}
