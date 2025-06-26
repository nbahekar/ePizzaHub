using ePizzaHub.Core.Interface;
using ePizzaHub.Models.ApiModels.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> MakePayment([FromBody] MakePaymentRequest paymentRequest)
        {

            if (ModelState.IsValid)
            {
                var result = await _paymentService.MakePayment(paymentRequest);
                return Ok();
            }

            return BadRequest("Pease check view");
        }
    }
}
