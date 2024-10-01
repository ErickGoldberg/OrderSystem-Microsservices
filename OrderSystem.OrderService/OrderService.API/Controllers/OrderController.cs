using Microsoft.AspNetCore.Mvc;

namespace OrderSystem.OrderService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] OrderRequest request)
        {
            // Lógica para criar o pedido
            // Enviar evento para mensageria para notificar Payment e Delivery
            return Ok("Order created");
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderStatus(string id)
        {
            // Lógica para buscar o status do pedido
            return Ok(new { Status = "Shipped" });
        }

        [HttpPost("cancel")]
        public IActionResult CancelOrder([FromBody] CancelRequest request)
        {
            // Lógica para cancelar o pedido
            // Enviar mensagem para mensageria para notificar Payment e Delivery
            return Ok("Order canceled");
        }
    }
}
