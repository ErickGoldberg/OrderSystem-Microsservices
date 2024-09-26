using Microsoft.AspNetCore.Mvc;

namespace OrderSystem.PaymentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
