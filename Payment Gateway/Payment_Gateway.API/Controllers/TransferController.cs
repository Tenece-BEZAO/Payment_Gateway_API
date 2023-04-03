using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment_Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(Summary = "Create Transfer using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public ActionResult<TransactionInitializeResponse> CreateTranserRecipient(TransferProcessRequest transferProcessRequest)
        {
            var response = _transferService.CreateTransferRecipient(transferProcessRequest);
            return Ok(response);
        }

    }
}
