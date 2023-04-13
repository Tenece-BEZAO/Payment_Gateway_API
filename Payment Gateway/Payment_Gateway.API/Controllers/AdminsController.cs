using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.Shared.DataTransferObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Payment_Gateway.API.Controllers
{
    [ApiController]
    [Route("api/admins")]
    public class AdminsController : ControllerBase
    {

        IAdminServices _adminServices;
        IAdminProfileServices _adminProfileServices;

        public AdminsController(IAdminServices adminServices, IAdminProfileServices adminProfileServices)
        {
            _adminServices = adminServices;
            _adminProfileServices = adminProfileServices;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminForRegistrationDto adminForRegistration)
        {

            var response = await _adminServices.RegisterAdmin(adminForRegistration);

            return Ok(response);

        }


        [HttpPost("createProfile")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProfile([FromBody] AdminProfileDto adminProfile)
        {
            try
            {
                var response = await _adminProfileServices.CreateProfile(adminProfile);

                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

        }


        [HttpPost("updateProfile")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProfile([FromBody] AdminProfileDto adminProfile)
        {
            try
            {
                var response = await _adminProfileServices.UpdateProfile(adminProfile);
                return Ok(response);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        //[Authorize]
        //[Route("list-bank")]
        [HttpGet("Check-Balance")]
        [SwaggerOperation(Summary = "Check account balance")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<object>> CheckBalance()
        {
            var response = await _adminServices.CheckBalance();
            return Ok(response);
        }


        //[Authorize]
        //[Route("list-bank")]
        [HttpGet("Check-Ledger")]
        [SwaggerOperation(Summary = "Check Ledger balance")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "successful", Type = typeof(SuccessResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "failed", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<object>> FetchLedger()
        {
            var response = await _adminServices.FetchLedger();
            return Ok(response);
        }
    }
}
