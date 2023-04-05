using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json.Linq;

namespace Payment_Gateway.API.Controllers
{
    [Route("PayGo/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {


        [AllowAnonymous]
        [HttpPost("cardPayment")]
        [SwaggerOperation(Summary = "Makes payment using your card")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Payment successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Payment failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> Payment(PaymentRequest request)
        {
            var payload = new
            {
                email = request.Email,
                amount = request.AmountInKobo,
                card = new
                {
                    number = request.cardNumber,
                    cvv = request.cvv,
                    expiry_month = request.expiryMonth,
                    expiry_year = request.expiryYear
                }
            };

            string apikey = "sk_test_6c6fc60af0119e14cad8cad7000eb9916014a998";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apikey);

            var response = await client.PostAsJsonAsync("https://api.paystack.co/charge", payload);

            string responseContent = await response.Content.ReadAsStringAsync();

            JObject responseObject = JObject.Parse(responseContent);

            return Ok(responseObject);
        }

    }
}
