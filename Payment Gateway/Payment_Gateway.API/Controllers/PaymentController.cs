using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.API.Extensions;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment_Gateway.API.Controllers
{
    [Route("PayGo/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        public readonly IMakePaymentService _paymentService;
        private readonly IWalletService _walletService;
        private readonly IPaymentServiceExtension _paymentServiceExtension;
        private readonly IHttpContextAccessor _contextAccessor;

        public PaymentController(IMakePaymentService paymentService, IWalletService walletService, IPaymentServiceExtension paymentServiceExtension, IHttpContextAccessor httpContextAccessor)
        {
            _paymentService = paymentService;
            _walletService = walletService;
            _paymentServiceExtension = paymentServiceExtension;
            _contextAccessor = httpContextAccessor;
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

            if (response.data.status == "success" || response != null)
            {
                string? userId = _contextAccessor.HttpContext?.User.GetUserId();
                int amount = response.data.amount;

                _ = await _walletService.UpdateBlance(userId,amount);
                _ = _paymentServiceExtension.CreatePayment(userId, response);
                return Ok(response);
            }
            return BadRequest();
        }
    }
}
