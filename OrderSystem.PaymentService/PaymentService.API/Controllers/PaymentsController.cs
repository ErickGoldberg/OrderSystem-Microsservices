using Microsoft.AspNetCore.Mvc;

namespace OrderSystem.PaymentService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        [HttpPost("create")]
        public IActionResult CreatePayment([FromBody] PaymentRequest request)
        {
            // Lógica para criar o pagamento
            // Enviar evento para mensageria para notificar o microsserviço de Order
            return Ok("Payment created");
        }

        [HttpGet("{id}")]
        public IActionResult GetPaymentStatus(string id)
        {
            // Lógica para buscar o status do pagamento
            return Ok(new { Status = "Paid" });
        }

        [HttpPost("refund")]
        public IActionResult RefundPayment([FromBody] RefundRequest request)
        {
            // Lógica para realizar o estorno do pagamento
            // Enviar mensagem para mensageria para notificar microsserviços relevantes
            return Ok("Refund initiated");
        }
    }
}
