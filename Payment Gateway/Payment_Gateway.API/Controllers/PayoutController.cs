using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.API.Extensions;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using Payment_Gateway.Shared.DataTransferObjects.Response;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment_Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayoutController : ControllerBase
    {
        private readonly IPayoutService _payoutService;
        private readonly IWalletService _walletService;
        private readonly IPayoutServiceExtension _TransactionService;
        private readonly IHttpContextAccessor _contextAccessor;

        public PayoutController(IPayoutService payoutService, IWalletService walletService, IPayoutServiceExtension payoutServiceExtension, IHttpContextAccessor httpContextAccessor)
        {
            _payoutService = payoutService;
            _walletService = walletService;
            _TransactionService = payoutServiceExtension;
            _contextAccessor = httpContextAccessor;
        }


        //[Authorize]
        //[Route("list-bank")]
        [HttpGet("Available-Banks")]
        [SwaggerOperation(Summary = "Select preferred bank")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Payment successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Payment failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<object>> ListBanks(string Currency)
        {
            ListBankResponse response = await _payoutService.ListBanks(Currency);
            return Ok(response);
        }

        //[Authorize]
        //[Route("resolve-accountnumber")]
        [HttpGet("Check-account")]
        [SwaggerOperation(Summary = "Resolve account using PayGo")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Operation successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Operation failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult> ResolveAccountNumber([FromQuery] ResolveAccountNumberRequest request)
        {
            ResolveBankResponse response = await _payoutService.ResolveAccountNumber(request);
            return Ok(response);
        }


        //[Authorize]
        //[Route("resolve-accountnumber")]
        [HttpPost("Create-recipient")]
        [SwaggerOperation(Summary = "Resolve account using PayGo")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Operation successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Operation failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult> CreateTransferRecipient(CreateRecipientRequest request)
        {
            CreateRecipientResponse response = await _payoutService.CreateTransferRecipient(request);
            return Ok(response);
        }



        //[Authorize]
        //[Route("initiate-transfer")]
        [HttpPost("Make-payout")]
        [SwaggerOperation(Summary = "Makes payment using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Payment successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Payment failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult> InitiateTransfer([FromBody] InitiateTransferRequest initiateTransfer)
        {
            TransferResponse response = await _payoutService.InitiateTransfer(initiateTransfer);
            if (response != null)
            {
                string? userId = _contextAccessor?.HttpContext?.User?.GetUserId();
                _ = _TransactionService.CreatePayout(userId, response);
                return Ok(response);
            }
            return BadRequest();
        }


        //[Authorize]
        //[Route("finalize-transfer")]
        [HttpPost("Finalize-Payment")]
        [SwaggerOperation(Summary = "Makes payment using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Payment successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Payment failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult> FinilizeTransfer(string transferIdOrCode)
        {
            FinalizeTransferResponse response = await _payoutService.FinilizeTransfer(transferIdOrCode);
           
            if (response.data.status == "success" || response != null)
            {
                string? userId = _contextAccessor.HttpContext?.User.GetUserId();
                int amount = int.Parse(response.data.amount) * (-1);

                _ = await _walletService.UpdateBlance(userId,amount );
                _ = _TransactionService.UpdatePayout(response);
                return Ok(response);
            }
           return BadRequest();
        }

    }
}
