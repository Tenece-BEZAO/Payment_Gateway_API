﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("Make card Payment")]
        [SwaggerOperation(Summary = "Makes payment using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Payment successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Payment failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<TransactionInitializeResponse>> ProcessPayment(DepositPaymentRequest processPayment)
        {
            var response = _paymentService.ProcessPayment(processPayment);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("Verify card Payment")]
        [SwaggerOperation(Summary = "Makes payment using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "verification successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "verification failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<TransactionVerifyResponse>> VerifyPayment(string reference)
        {
            var verifypayment = _paymentService.VerifyPayment(reference);
            return Ok(verifypayment);
        }


        [AllowAnonymous]
        [HttpPost("Get all Payment")]
        [SwaggerOperation(Summary = "Get all payment using paystack")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<TransactionVerifyResponse>> AllPayments(TransactionVerifyResponse processPayment)
        {
            var verifypayment = _paymentService.AllPayments();
            return Ok(verifypayment);
        }

      
    }
}
