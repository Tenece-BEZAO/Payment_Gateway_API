using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment_Gateway.API.Controllers
{
    [Route("PayGo/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        IMakePaymentService _paymentService;

        public PaymentController(IMakePaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [AllowAnonymous]
        [HttpPost("cardPayment")]
        [SwaggerOperation(Summary = "Makes payment using your card")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Payment successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Payment failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult> MakePayment([FromBody] PaymentRequest makePayment)
        {
            var response = await _paymentService.MakePayment(makePayment);
            return Ok(response);
        }
    }
}
