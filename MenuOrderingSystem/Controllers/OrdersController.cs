using MenuOrderingSystem.Models;
using MenuOrderingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MenuOrderingSystem.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderRequest request)
        {
            var order = await _orderService.CreateOrder(request);
            if (!order.IsValid)
            {
                return BadRequest(order.Display);
            }

            return Ok(order.Display);
        }
    }
}
